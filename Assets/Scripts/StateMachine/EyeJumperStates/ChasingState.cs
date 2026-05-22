using System;
using UnityEngine;

public class ChasingState : BaseState
{
    private RangedEnemy eye;
    private StateMachine stateMachine;

    private float adaptedStrenght;
    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        if (!eye.Player) return;

        var playerPosition = eye.Player.transform.position;
        var distance = Vector3.Distance(playerPosition, eye.transform.position);


        if (distance <= eye.attackRange)
        {
            float minForce = eye.GetJumpStrenght() * 0.2f;
            float maxForce = eye.GetJumpStrenght();

            float t = Mathf.Clamp01(distance / eye.attackRange);
            float adaptedStrenght = Mathf.Lerp(minForce, maxForce, t);

            stateMachine.SetParam("adaptedStrenght", adaptedStrenght);
            stateMachine.TransitionTo<PreparingAttackState>();
        }
        else if (distance >= eye.GetDetectRange())
        {
            stateMachine.TransitionTo<IdleState>();
        }
        else
        {
            FollowPlayer(playerPosition);
        }
    }

    protected void FollowPlayer(Vector3 playerPosition)
    {
        if (!eye._isTouching)
        {
            eye.transform.position = Vector3.MoveTowards(eye.transform.position, playerPosition, eye.GetMoveSpeed() * Time.deltaTime);
        }
    }

    public override void OnEnd()
    {

    }
}