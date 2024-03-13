public abstract class BaseState
{
    protected bool _isRootState = false;
    protected virtual StateMachine _ctx { get; private set; }
    protected StateFactory _factory { get; private set; }
    protected BaseState _currentSuperState;
    protected BaseState _currentSubState;


    public BaseState(StateMachine currentContext, StateFactory stateFactory)
    {
        _ctx = currentContext;
        _factory = stateFactory;
    }
    
    public abstract void EnterState();

    protected abstract void UpdateState();

    public abstract void ExitState();

    protected abstract void CheckSwitchStates();
    
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        _currentSubState?.UpdateStates();
    }
    
    protected void SwitchState(BaseState newState)
    {
        // Current State Exits State
        ExitState();
        
        // New state enters state
        newState.EnterState();

        if (_isRootState)
        {
            // switch current state of context
            _ctx.CurrentState = newState;
        }
        else
        {
            _currentSuperState?.SetSubState(newState);
        }
    }
    
    protected void SetSuperState(BaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(BaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
