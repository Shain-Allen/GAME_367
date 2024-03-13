using System;
using System.Reflection;

public class StateFactory
{
    private StateMachine _contex;

    public StateFactory(StateMachine currentContext)
    {
        _contex = currentContext;
    }

    // This is used to create all the states.
    // its used whenever you wish to change states
    public BaseState GetState<TState>() where TState : BaseState
    {
        // Check if T has the Constructor for class BaseState
        ConstructorInfo constructorInfo = typeof(TState).GetConstructor(new[] { typeof(StateMachine), typeof(StateFactory) });

        if (constructorInfo == null) throw new InvalidOperationException($"type of {typeof(TState)} does not have the constructor for BaseState");
        
        // If T does have the Constructor invoke it the constructor
        TState instance = (TState)constructorInfo.Invoke(new object[] { _contex, this });
        return instance;
    }
}