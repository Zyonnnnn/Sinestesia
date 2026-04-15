using UnityEngine;

public class IdleState : BaseState
{
    private RangedEnemy eye;
    private StateMachine stateMachine;
    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine =  stateMachine;
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        if (!eye.Player) return;

        var playerPosition = eye.Player.transform.position;
        var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, eye.transform.position));

        if (distanceFromPlayer <= eye.GetDetectRange())
        {
            stateMachine.TransitionTo<ChasingState>();
        }
    }

    public override void OnEnd()
    {
    }
}
