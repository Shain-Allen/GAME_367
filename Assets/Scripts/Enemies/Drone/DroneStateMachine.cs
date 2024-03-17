using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneStateMachine : StateMachine
{
    private NavMeshAgent _navMeshAgent;
    
    [Header("patrol Points")]
    public List<Transform> _patrolPoints;
    
    [Header("Enemy Movement Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _miniumAttackrange;
    [SerializeField] private float _maximumAttackrange;
    [SerializeField] private float _provocationRange;
    [SerializeField] private LayerMask _obstaclesLayerMask;
    public NavMeshHit _playerNavMeshHit;
    
    [Header("Enemy Attack Parameters")]
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private GameObject _bulletPrefab;
    

    private void Awake()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
        _navMeshAgent.angularSpeed = _turnSpeed;
        _navMeshAgent.acceleration = _acceleration;
        _navMeshAgent.stoppingDistance = _miniumAttackrange;
    }

    private void Start()
    {
        CurrentState = _states.GetState<DronePatrolState>();
        CurrentState.EnterState();
    }
    
    private void Update()
    {
        CurrentState.UpdateStates();

        _navMeshAgent.Raycast(GameManager.Player.transform.position, out _playerNavMeshHit);
    }
    
    public void SetDroneDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }
}
