using UnityEngine;

public class StateMachine
{
    public BaseState CurrentState { get; private set; }
    
    private GameObject gameObject;
    
    public StateMachine(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    
    public void TransitionTo<T>() where T: BaseState, new()
    {
        CurrentState?.OnEnd();
        CurrentState = new T();
        CurrentState.OnStart(gameObject, this);
    }

    public void OnTick()
    {
        CurrentState?.OnTick();
    }
}