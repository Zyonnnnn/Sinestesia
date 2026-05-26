using UnityEditor;
using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    Rigidbody playerRb;
    StateMachine stateMachine;

    LighterBehaviour lighter;

    float baseDistanceX = 0.5f;
    float baseDistanceZ = 0.2f;

    float velX, velZ;

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

            velX = playerRb.linearVelocity.x > 0f ? baseDistanceX : -baseDistanceX;
            velZ = playerRb.linearVelocity.z > 0f ? baseDistanceZ : -baseDistanceZ;

            lighter.gameObject.transform.rotation = playerRb.linearVelocity.x != 0f ? Quaternion.Slerp(lighter.gameObject.transform.rotation, rotationX, 12 * Time.deltaTime) : Quaternion.Slerp(lighter.gameObject.transform.rotation, rotationZ, 12 * Time.deltaTime);
        }

        lighter.gameObject.transform.position = playerPos.position + new Vector3(velX, 0, velZ);
    }

    public override void OnEnd()
    {

    }
}