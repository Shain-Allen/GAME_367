using System;

public class DronePatrolState : BaseState
{
    private new readonly DroneStateMachine _droneCtx;
    
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
        _droneCtx.SetDroneDestination(_droneCtx._patrolPoints[randomPatrolPoint].position);
    }

    protected override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    protected override void CheckSwitchStates()
    {
        throw new NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new NotImplementedException();
    }
}