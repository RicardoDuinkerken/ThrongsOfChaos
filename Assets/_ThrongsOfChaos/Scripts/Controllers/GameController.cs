using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //singleton
    private static GameController _instance;
    
    //getter
    public static GameController instance 
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GameController is null");
            }

            return _instance;
        }
    }
    
    //public variables
    public event Action OnGameStart;
    public event Action onGameRestart;
    public event Action OnGameEnd;
    public event Action<int> OnCrystalDamaged;
    public event Action OnCrystalDestroyed;
    
    //serialized variables
    [SerializeField] private int crystalHealth;
    
    //private variables
    private int _currentCrystalHealth;

    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        
    }

    private void GameStart()
    {
        _currentCrystalHealth = crystalHealth;
        OnGameStart?.Invoke();
    }
    public void GameRestart()
    {
        onGameRestart?.Invoke();
    }
    public void GameEnd()
    {
        OnGameEnd?.Invoke();
    }

    public void CrystalDamaged(int damage)
    {
        _currentCrystalHealth -= damage;
        if (_currentCrystalHealth <= 0)
        {
            OnCrystalDestroyed?.Invoke();
        }
        OnCrystalDamaged?.Invoke(damage);
    }


}
