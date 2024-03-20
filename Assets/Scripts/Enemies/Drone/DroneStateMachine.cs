using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneStateMachine : StateMachine
{
    [HideInInspector] public NavMeshAgent _navMeshAgent;
    [HideInInspector] public DroneGun _droneGun;
    
    [Header("patrol Points")]
    public List<Transform> _patrolPoints;
    
    [Header("Enemy Movement Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private LayerMask _obstaclesLayerMask;
    public NavMeshHit _playerNavMeshHit;
    public RaycastHit _playerRaycastHit;
    public bool _isPlayerVisible;
    
    [Header("Enemy Attack Parameters")]
    public float _miniumAttackrange;
    public float _maximumAttackrange;
    public float _provocationRange;
    

    private void Awake()
    {
        _states = new StateFactory(this);
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _droneGun = gameObject.GetComponent<DroneGun>();
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

        // do a Physics raycast from the enemy in the direction of the player to check if the player is visible to the enemy
        Vector3 direction = GameManager.Player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction.normalized, out _playerRaycastHit))
        {
            _isPlayerVisible = _playerRaycastHit.transform == GameManager.Player.transform;
        }
        
        _navMeshAgent.Raycast(GameManager.Player.transform.position, out _playerNavMeshHit);
        
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = GameManager.Player.transform.position - transform.position;
        
        Gizmos.DrawLine(transform.position, transform.position + direction.normalized * _provocationRange);
    }
}
