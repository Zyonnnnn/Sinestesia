using UnityEditor;
using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    Rigidbody playerRb;
    StateMachine stateMachine;

    InputManager inputManager;
    LighterBehaviour lighter;
    ParticleSystem ps;

    float baseDistanceX = 0.5f;
    float baseDistanceZ = 0.2f;

    float velX, velZ;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
        ps = gameObject.GetComponent<ParticleSystem>();
        inputManager = new InputManager();

        PlayerBehaviour.OnPicked += HandlePicked;
    }

    private void HandlePicked()
    {
        if (PlayerBehaviour.canInteract)
        {
            lighter.gameObject.GetComponent<BoxCollider>().enabled = !lighter.gameObject.GetComponent<BoxCollider>().enabled;

            if (ps != null)
            {
                if (!ps.isEmitting)
                {
                    ps.Play();
                }
                else
                {
                    ps.Stop();
                }
            }
        }
        else
        {
            stateMachine.TransitionTo<FreeState>();
        }
    }

    public override void OnTick()
    {
        Debug.Log(inputManager.GetInputDirection());

        if (stateMachine.HasParam("PlayerPos") && stateMachine.HasParam("PlayerRigidbody"))
        {
            playerPos = stateMachine.GetParam<Transform>("PlayerPos");
            playerRb = stateMachine.GetParam<Rigidbody>("PlayerRigidbody");
        }

        if (inputManager.GetInputDirection().magnitude != 0f)
        {
            var rotationX = inputManager.GetInputDirection().x < 0f ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            var rotationZ = inputManager.GetInputDirection().y < 0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);

            velX = inputManager.GetInputDirection().x > 0f ? baseDistanceX : -baseDistanceX;
            velZ = inputManager.GetInputDirection().y > 0f ? baseDistanceZ : -baseDistanceZ;

            lighter.gameObject.transform.rotation = inputManager.GetInputDirection().x != 0f ? Quaternion.Slerp(lighter.gameObject.transform.rotation, rotationX, 12 * Time.deltaTime) : Quaternion.Slerp(lighter.gameObject.transform.rotation, rotationZ, 12 * Time.deltaTime);
        }
        else
        {
            velX = 0f;
            velZ = 0f;
        }

        lighter.gameObject.transform.position = playerPos.position + new Vector3(velX, 0, velZ);
    }

    public override void OnEnd()
    {

    }
}