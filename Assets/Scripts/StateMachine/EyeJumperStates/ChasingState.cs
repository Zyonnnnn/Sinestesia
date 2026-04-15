using System;
using UnityEngine;

public class ChasingState : BaseState
{
    private RangedEnemy eye;
    private StateMachine stateMachine;
    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        if (!eye.Player) return;

        CheckPlayerPosition();
        CheckPlayerDistance(CheckPlayerPosition());

        HandleDistance(CheckPlayerDistance(CheckPlayerPosition()));
        
        FollowPlayer(CheckPlayerPosition());
    }

    private float CheckPlayerDistance(Vector3 playerPosition)
    {
        return Mathf.Abs( Vector3.Distance(playerPosition, eye.transform.position));
    }

    private Vector3 CheckPlayerPosition()
    {
        return eye.Player.transform.position;
    }

    protected void HandleDistance(float distanceFromPlayer)
    {
        if (distanceFromPlayer <= eye.attackRange)
        {
            stateMachine.TransitionTo<PreparingAttackState>();
        }
        else if (distanceFromPlayer >= eye.GetDetectRange())
        {
            stateMachine.TransitionTo<IdleState>();
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