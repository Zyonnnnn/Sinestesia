using System;
using UnityEditor;
using UnityEngine;

public class FreeState : BaseState
{
    LighterBehaviour lighter;
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
    }

    public override void OnTick()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
