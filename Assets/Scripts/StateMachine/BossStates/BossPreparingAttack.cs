using UnityEngine;

public class BossPreparingAttack : BaseState
{
    private MeleeEnemy boss;
    private TentacleBehaviour tentacle;

    private StateMachine stateMachine;

    float timer = 1f;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
    }

    public override void OnTick()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.TransitionTo<BossAttackingState>();
        }
    }

    public override void OnEnd()
    {

    }
}
