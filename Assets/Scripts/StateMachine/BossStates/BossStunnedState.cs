using UnityEngine;

public class BossStunnedState : BaseState
{
    private MeleeEnemy boss;
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        boss = gameObject.GetComponent<MeleeEnemy>();

        Debug.Log("Boss is stunned!");
    }

    public override void OnTick()
    {

    }

    public override void OnEnd()
    {

    }
}
