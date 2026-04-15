using UnityEngine;

public abstract class BaseState
{
    public abstract void OnStart(GameObject gameObject, StateMachine stateMachine);
    
    public abstract void OnTick();
    
    public abstract void OnEnd();
}