using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    LighterBehaviour lighter;
    StateMachine stateMachine;

    Vector3 distance = new(2, 0, 0);

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
    }

    public override void OnTick()
    {
        if (stateMachine.HasParam("PlayerPos"))
        {
            playerPos = stateMachine.GetParam<Transform>("PlayerPos");
        }

        lighter.gameObject.transform.position = playerPos.position + distance;

        if (PlayerBehaviour.hold)
        {
            stateMachine.TransitionTo<FreeState>();
        }
    }

    public override void OnEnd()
    {
    }
}
