using UnityEngine;

public class BossAttackingState : BaseState
{
    private TentacleBehaviour tentacle;
    private StateMachine stateMachine;
    private Transform bossRoot;
    private Collider tentacleCollider;
    private int groundMask;
    private readonly Collider[] groundHits = new Collider[8];

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        bossRoot = gameObject.transform;

        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
        tentacleCollider = tentacle.GetComponent<Collider>();

        groundMask = LayerMask.GetMask("groundtest");
    }

    public override void OnTick()
    {
        if (IsTouchingGround())
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

    private bool IsTouchingGround()
    {
        Bounds bounds = tentacleCollider.bounds;
        int hitCount = Physics.OverlapBoxNonAlloc(
            bounds.center,
            bounds.extents,
            groundHits,
            Quaternion.identity,
            groundMask,
            QueryTriggerInteraction.Ignore);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = groundHits[i];

            if (hit != tentacleCollider && !hit.transform.IsChildOf(bossRoot))
            {
                return true;
            }
        }

        return false;
    }
}
