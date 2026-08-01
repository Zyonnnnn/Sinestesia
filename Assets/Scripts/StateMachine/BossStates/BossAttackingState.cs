using UnityEngine;
using UnityEditor;

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
        Bounds bounds = collider.bounds;

        bool colliding = Physics.CheckBox(bounds.center, bounds.extents, collider.transform.rotation, LayerMask.GetMask("groundtest"));

        if (colliding)
        {
            stateMachine.TransitionTo<BossStunnedState>();
        }
        else
        {
            Debug.Log("caindo");
            tentacle.transform.position += Vector3.down * 25f * Time.deltaTime;
        }
    }

    public override void OnEnd()
    {

    }
}