using UnityEngine;
using UnityEngine.Rendering.RendererUtils;

public class ChasingState : BaseState
{
    private RangedEnemy eye;
    private StateMachine StateMachine;
    public override void OnStart(GameObject gameObject)
    {
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        var playerPosition = Object.FindFirstObjectByType<PlayerBehaviour>().transform.position;
        //var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, eye.transform.position));

        //if (distanceFromPlayer <= eye.attackRange)
        //{
        //    StateMachine.TransitionTo<PreparingAttackState>();
        //}
        
        FollowPlayer(playerPosition);
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