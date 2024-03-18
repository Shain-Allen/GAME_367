using System;

public class DronePatrolState : BaseState
{
    private readonly DroneStateMachine _droneCtx;
    
    public DronePatrolState(StateMachine currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
    {
        _isRootState = true;
        _droneCtx = (DroneStateMachine)currentContext;
    }

    public override void EnterState()
    {
        if (_droneCtx._patrolPoints.Count <= 0) return;
        
        //Set a random patrol point
        int randomPatrolPoint = UnityEngine.Random.Range(0, _droneCtx._patrolPoints.Count);
        _droneCtx._navMeshAgent.SetDestination(_droneCtx._patrolPoints[randomPatrolPoint].position);
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
        
        //Once the drone has reached the current destination point, set the next destination point to a random patrol point
        if (!(_droneCtx._navMeshAgent.remainingDistance <= _droneCtx._navMeshAgent.stoppingDistance)) return;
        
        int randomPatrolPoint = UnityEngine.Random.Range(0, _droneCtx._patrolPoints.Count);
        _droneCtx._navMeshAgent.SetDestination(_droneCtx._patrolPoints[randomPatrolPoint].position);
    }

    public override void ExitState()
    {
        
    }

    protected override void CheckSwitchStates()
    {
        if (_droneCtx._playerNavMeshHit.distance <= _droneCtx._provocationRange)
        {
            SwitchState(_factory.GetState<DroneAttackState>());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}