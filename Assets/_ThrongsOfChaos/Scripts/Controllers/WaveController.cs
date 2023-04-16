using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //serialized
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform target;

    //private
    private int _enemiesAlive;
    private int _timeBetweenWaves;
    private int _currentWave;

    private bool _isWaveStarted = false;
    
    //class
    [System.Serializable]
    public class Wave
    {
        public float spawnPointRadius, spawnRate;
        public EnemyLocation[] enemyLocations;
        

        [System.Serializable]
        public struct EnemyLocation
        {
            public GameObject enemyPrefab;
            public Transform spawnLocation;
        }
    }
    
    private void Awake()
    {
        GameController.instance.OnGameStart += StartWaves;
        GameController.instance.OnRoundStart += NextWave;
    }
    
    private void StartWaves()
    {
        if (!_isWaveStarted)
        {
            _isWaveStarted = true;
            Debug.Log("Start Wave Spawn");
            StartCoroutine(SpawnWave(0));
        }
    }
    
    private void NextWave(int waveNr)
    {
        if (_isWaveStarted)
        {
            StartCoroutine(SpawnWave(waveNr));
        }
    }
    
    private IEnumerator SpawnWave(int waveNr)
    {
        Wave wave = waves[waveNr];
        _enemiesAlive = wave.enemyLocations.Length-1;
        _currentWave = waveNr;
    
        foreach (Wave.EnemyLocation enemyLocation in wave.enemyLocations)
        {
            SpawnEnemy(enemyLocation.spawnLocation, wave.spawnPointRadius, enemyLocation.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
    }
    
    private IEnumerator WaveFinished()
    {
        Debug.Log("wave Finished");
        if (_currentWave >= waves.Count-1)
        {
            GameController.instance.GameEnd();
        }
        else
        {
            yield return new WaitForSeconds(_timeBetweenWaves);
            GameController.instance.StartRound(_currentWave + 1);
        }
    }

    private void SpawnEnemy(Transform spawnPoint, float spawnRadius, GameObject enemyPrefab)
    {
        var offsetZ = Random.Range(-spawnRadius, spawnRadius);
        var offsetX = Random.Range(-spawnRadius, spawnRadius);
        Vector3 originalPosition = spawnPoint.position;
        Vector3 position = new Vector3(originalPosition.x + offsetX, originalPosition.y, originalPosition.z + offsetZ);
        GameObject enemy = Instantiate(enemyPrefab, position, spawnPoint.rotation);
        var enemyComp = enemy.GetComponent<Enemy>();
        enemyComp.Target = target;
        enemyComp.OnDeath += OnEnemyExpired;
        enemyComp.OnGoalReached += OnEnemyExpired;
    }

    private void OnEnemyExpired()
    {
        _enemiesAlive--;
        if (_enemiesAlive <= 0)
        {
            StartCoroutine(WaveFinished());
        }
    }
}
