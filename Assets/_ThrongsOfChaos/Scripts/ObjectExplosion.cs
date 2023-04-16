using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectExplosion : MonoBehaviour
{
    [SerializeField] private GameObject[] shatteredPieces;
    [SerializeField] private float minForce, maxForce, radius;

    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    public List<Rigidbody> GetRbShatteredPieces
    {
        get { return _rigidbodies;  }
    }

   
    
    private void Awake()
    {
        TryAddRigidbody();
    }

    public void Explode()
    {
        foreach (var rb in _rigidbodies)
        {
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
            }

            rb.useGravity = true;
            rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
        }
    }
    
    

    private void TryAddRigidbody()
    {
        foreach (var piece in shatteredPieces)
        {
            var rb = piece.GetComponent<Rigidbody>();
            
            if (!rb)
            {
                piece.AddComponent<Rigidbody>();
                rb = piece.GetComponent<Rigidbody>();
            }
            
            _rigidbodies.Add(rb);
        }
    }
}
