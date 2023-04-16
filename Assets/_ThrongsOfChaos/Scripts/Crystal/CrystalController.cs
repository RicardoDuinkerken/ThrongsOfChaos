using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private GameObject crystalVisual;
    
    private void Start()
    {
        GameController.instance.OnCrystalDestroyed += CrystalDestroyed;
        GameController.instance.onGameRestart += CrystalReset;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //enemy should die
            //other.GetComponent(Enemy).GoalReached;
            
            GameController.instance.CrystalDamaged(1);
        }
    }
    
    private void CrystalDestroyed()
    {
        crystalVisual.SetActive(false);
    }
    
    private void CrystalReset()
    {
        crystalVisual.SetActive(true);
    }
}
