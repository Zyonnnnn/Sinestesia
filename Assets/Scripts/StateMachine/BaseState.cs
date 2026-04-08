using UnityEngine;

public abstract class BaseState
{
    public abstract void OnStart(GameObject gameObject);
    
    public abstract void OnTick();
    
    public abstract void OnEnd();
}