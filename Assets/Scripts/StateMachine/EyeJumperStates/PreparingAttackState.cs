using UnityEngine;

public class PreparingAttackState : BaseState
{
    private readonly float preparingTime = 1;
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
        if (timer >= preparingTime)
        {
            stateMachine.TransitionTo<AttackingState>();
            timer = 0;
        }
    }

    public override void OnEnd()
    {
        
    }
}