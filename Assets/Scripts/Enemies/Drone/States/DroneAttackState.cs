using System;

public class DroneAttackState : BaseState
{
    public DroneAttackState(StateMachine currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
    {
        _isRootState = true;
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