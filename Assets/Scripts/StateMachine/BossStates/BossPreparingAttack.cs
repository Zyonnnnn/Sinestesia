using UnityEngine;

public class BossPreparingAttack : BaseState
{
    private StateMachine stateMachine;

    float timer = 1f;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnTick()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            BossChasingState.isMoving = false;
            stateMachine.TransitionTo<BossAttackingState>();
        }
    }

    public override void OnEnd()
    {

    }
}
