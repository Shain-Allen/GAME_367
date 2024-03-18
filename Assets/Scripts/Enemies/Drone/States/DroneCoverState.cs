using System;

public class DroneCoverState : BaseState
{
    private readonly DroneStateMachine _droneCtx;
    
    public DroneCoverState(StateMachine currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
    {
        _isRootState = true;
        _droneCtx = (DroneStateMachine)currentContext;
    }

    public override void EnterState()
    {
        throw new NotImplementedException();
    }

    protected override void UpdateState()
    {
        throw new NotImplementedException();
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