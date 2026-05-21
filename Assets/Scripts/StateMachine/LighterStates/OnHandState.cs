using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    StateMachine stateMachine;
    
    LighterBehaviour lighter;

    Vector3 distance = new(2, 0, 0);

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
        
        PlayerBehaviour.OnPicked += HandlePicked;
    }

    private void HandlePicked()
    {
        if (PlayerBehaviour.canInteract)
        {
            
        }
        else
        {
            stateMachine.TransitionTo<FreeState>();
        }
    }

    public override void OnTick()
    {
        if (stateMachine.HasParam("PlayerPos"))
        {
            playerPos = stateMachine.GetParam<Transform>("PlayerPos");
        }

        lighter.gameObject.transform.position = playerPos.position + distance;

        Debug.Log("to na mao");
    }

    public override void OnEnd()
    {
        
    }
}