using UnityEditor;
using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    Rigidbody playerRb;
    StateMachine stateMachine;

    LighterBehaviour lighter;

    Vector3 baseDistanceX = new(0.5f, 0f, 0f);
    Vector3 baseDistanceZ = new(0f, 0f, 0.2f);

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
            lighter.gameObject.GetComponent<BoxCollider>().enabled = !lighter.gameObject.GetComponent<BoxCollider>().enabled;
        }
        else
        {
            stateMachine.TransitionTo<FreeState>();
        }
    }

    public override void OnTick()
    {
        if (stateMachine.HasParam("PlayerPos") && stateMachine.HasParam("PlayerRigidbody"))
        {
            playerPos = stateMachine.GetParam<Transform>("PlayerPos");
            playerRb = stateMachine.GetParam<Rigidbody>("PlayerRigidbody");
        }

        if (playerRb.linearVelocity.magnitude != 0f)
        {
            var rotationX = playerRb.linearVelocity.x < 0f ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            var rotationZ = playerRb.linearVelocity.z < 0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);

            lighter.gameObject.transform.rotation = playerRb.linearVelocity.x != 0f ? rotationX : rotationZ;

        }
    }

    public override void OnEnd()
    {

    }
}