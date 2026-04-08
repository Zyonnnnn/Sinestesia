using UnityEngine;

public class IdleState : BaseState
{
    private RangedEnemy eye;
    private StateMachine StateMachine;
    public override void OnStart(GameObject gameObject)
    {
        eye = gameObject.GetComponent<RangedEnemy>();
        Debug.Log("acordei baby");
    }

    public override void OnTick()
    {
        var playerPosition = Object.FindFirstObjectByType<PlayerBehaviour>().transform.position;
        var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, eye.transform.position));

        if (distanceFromPlayer <= eye.GetDetectRange())
        {
            
        }
    }

    public override void OnEnd()
    {
    }
}
