using UnityEngine;

public class StunnedState : BaseState
{
    private readonly float stunTime = 1.5f;
    private float timer;
    
    private RangedEnemy eye;
    private StateMachine stateMachine;
    
    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine =  stateMachine;
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        timer += Time.deltaTime;
        if (timer >= stunTime)
        {
            stateMachine.TransitionTo<IdleState>();
            timer = 0;
        }
    }

    public override void OnEnd()
    {
        
    }
}