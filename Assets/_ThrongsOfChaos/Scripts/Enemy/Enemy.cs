using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IArrowHittable
{
    //serialized
    [SerializeField] private NavMeshAgent navmesh;
    [SerializeField] private Animator anim;
    [SerializeField] private float forceAmount;

    private bool _isDead = false;
    private bool _isMoving = false;
    
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
            if (!_isMoving)
            {
                _isMoving = true;
                anim.SetBool("IsMoving", _isMoving);
            }

            if (_isMoving)
            {
                anim.SetFloat("MoveSpeed", navmesh.velocity.magnitude / navmesh.speed);
            }
        }
    }

    public void GoalReached()
    {
        //Debug.Log("Enemy Reached Goal");
        GameController.instance.CrystalDamaged(1);
        OnGoalReached?.Invoke();
        Destroy(transform.gameObject);
    }

    public IEnumerator Die()
    {
        //Debug.Log("dying");
        OnDeath?.Invoke();
        
        _isDead = true;
        _isMoving = false;
        anim.SetBool("Death", _isDead);
        yield return new WaitForSeconds(1f);
        Destroy(transform.gameObject);
    }

    public void Hit(Arrow arrow)
    {
        navmesh.enabled = false;
        Destroy(arrow.gameObject);
        StartCoroutine(Die());
    }
    
    private void DisableCollider(Arrow arrow)
    {
        if (arrow.TryGetComponent(out Collider collider))
            collider.enabled = false;
    }
}
