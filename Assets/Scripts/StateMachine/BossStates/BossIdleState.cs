using UnityEngine;

public class BossIdleState : BaseState
{
    private MeleeEnemy boss;
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        boss = gameObject.GetComponent<MeleeEnemy>();
    }

    public override void OnTick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            stateMachine.TransitionTo<BossChasingState>();
        }
    }

    public override void OnEnd()
    {

    }
}
