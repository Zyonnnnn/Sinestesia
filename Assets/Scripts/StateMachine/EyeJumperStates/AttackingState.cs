using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : BaseState
{
    private StateMachine stateMachine;
    private Rigidbody eyeRb;
    private RangedEnemy eye;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        eye = gameObject.GetComponent<RangedEnemy>();
        eyeRb = gameObject.GetComponent<Rigidbody>();

        eyeRb.AddForce(new(0, 8, 0), ForceMode.Impulse);

        eye.OnLanded += HandleLanded;
    }

    private void HandleLanded()
    {
        stateMachine.TransitionTo<StunnedState>();
    }

    public override void OnTick()
    {
        
    }

    public override void OnEnd()
    {
        eye.OnLanded -= HandleLanded;
    }
}