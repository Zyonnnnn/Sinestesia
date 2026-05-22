using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseState CurrentState { get; private set; }

    private Dictionary<string, object> parameters = new();

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

    public void SetParam<T>(string key, T value)
    {
        parameters[key] = value;
    }

    public T GetParam<T>(string key, T defaultValue = default)
    {
        if (parameters.TryGetValue(key, out object value) && value is T typedValue)
        {
            return typedValue;
        }
        return defaultValue;
    }

    public bool HasParam(string key) => parameters.ContainsKey(key);

    public void ClearParams() => parameters.Clear();

    public void OnTick()
    {
        CurrentState?.OnTick();
    }
}