using UnityEngine;

public class BossPreparingAttack : BaseState
{
    private MeleeEnemy boss;
    private TentacleBehaviour tentacle;

    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
    }

    public override void OnTick()
    {

    }

    public override void OnEnd()
    {

    }
}
