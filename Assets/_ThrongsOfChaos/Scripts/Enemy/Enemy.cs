using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //serialized
    [SerializeField] private NavMeshAgent navmesh;
    [SerializeField] private Animator anim;

    private bool _isDead = false;
    
    //public
    public event Action OnDeath;
    public event Action OnGoalReached;

    //private
    [SerializeField]
    private Transform _target;
    
    //getter & setter
    public Transform Target
    {
        set
        {
            _target = value;
            navmesh.SetDestination(_target.position);
        }
    }

    private void Awake()
    {
        if (navmesh == null)
        {
            navmesh = GetComponent<NavMeshAgent>();
        }

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        navmesh.SetDestination(_target.position);
    }

    private void Update()
    {
        if (!_isDead)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("MoveSpeed", navmesh.velocity.magnitude / navmesh.speed);
        }
    }

    public void GoalReached()
    {
        //Debug.Log("Enemy Reached Goal");
        GameController.instance.CrystalDamaged(1);
        OnGoalReached?.Invoke();
        Destroy(transform.gameObject);
    }

    public void Die()
    {
        //Debug.Log("dying");
        OnDeath?.Invoke();
        Destroy(transform.gameObject);
    }
}
