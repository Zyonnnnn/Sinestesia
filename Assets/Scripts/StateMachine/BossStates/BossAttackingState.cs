using UnityEngine;

public class BossAttackingState : BaseState
{
    private MeleeEnemy boss;
    private TentacleBehaviour tentacle;

    private StateMachine stateMachine;

    Collider collider;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();

        collider = tentacle.GetComponent<Collider>();
    }

    public override void OnTick()
    {
        if (collider.providesContacts)
        {
            stateMachine.TransitionTo<BossStunnedState>();
        }
        else
        {
            Debug.Log("caindo");
            tentacle.transform.position = new(tentacle.transform.position.x, tentacle.transform.position.y * -0.0001f * Time.deltaTime, tentacle.transform.position.z);
        }
    }

    public override void OnEnd()
    {

    }
}

//FUI LEVA MEU CACHORRO PRA PASSEA

