using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    [SerializeField] private EnemyPatrol enemyPatrol;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(SetNewPatrolPoint());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(_navMeshAgent.remainingDistance);
        if (_navMeshAgent.remainingDistance < 1f)
        {
            StartCoroutine(SetNewPatrolPoint());
        }
    }

    private IEnumerator SetNewPatrolPoint()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
        _navMeshAgent.SetDestination(enemyPatrol.RandomPatrolPoint);
    }
}
