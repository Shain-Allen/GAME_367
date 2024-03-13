using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected StateFactory _states;
    public BaseState CurrentState { get; set; }
}
