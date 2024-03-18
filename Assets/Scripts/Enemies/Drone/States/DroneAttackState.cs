using System;

public class DroneAttackState : BaseState
{
    private readonly DroneStateMachine _droneCtx;
    
    public DroneAttackState(StateMachine currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
    {
        _isRootState = true;
        _droneCtx = (DroneStateMachine)currentContext;
    }

    public override void EnterState()
    {
        _droneCtx._navMeshAgent.SetDestination(_droneCtx._playerNavMeshHit.position);
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
        
        // if the player in within the attack range, attack
        if (_droneCtx._navMeshAgent.remainingDistance <= _droneCtx._maximumAttackrange)
        {
            _droneCtx._navMeshAgent.SetDestination(_droneCtx._playerNavMeshHit.position);
            _droneCtx._droneGun.Shoot();
        }
    }

    public override void ExitState()
    {
        
    }

    protected override void CheckSwitchStates()
    {
        // if the player is beyond the max attack range of the enemy switch back to Patrol State
        if (_droneCtx._navMeshAgent.remainingDistance > _droneCtx._miniumAttackrange)
        {
            SwitchState(_factory.GetState<DronePatrolState>());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}