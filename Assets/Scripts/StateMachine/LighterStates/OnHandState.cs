using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    StateMachine stateMachine;

    InputManager inputManager;
    LighterBehaviour lighter;
    ParticleSystem ps;

    float baseDistanceX = 0.6f;
    float baseDistanceZ = 0.2f;

    Vector3 holdOffset;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
        ps = gameObject.GetComponent<ParticleSystem>();
        inputManager = new InputManager();
        holdOffset = new Vector3(baseDistanceX, 0f, 0f);

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
        if (stateMachine.HasParam("PlayerPos"))
        {
            playerPos = stateMachine.GetParam<Transform>("PlayerPos");
        }

        if (playerPos == null)
        {
            return;
        }

        Vector2 inputDirection = inputManager.GetInputDirection();

        if (inputDirection.sqrMagnitude > 0f)
        {
            Quaternion targetRotation;

            if (Mathf.Abs(inputDirection.x) >= Mathf.Abs(inputDirection.y))
            {
                targetRotation = inputDirection.x < 0f ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
                holdOffset = new Vector3(inputDirection.x > 0f ? baseDistanceX : -baseDistanceX, 0f, 0f);
            }
            else
            {
                targetRotation = inputDirection.y < 0f ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);
                float sideOffset = holdOffset.x != 0f ? holdOffset.x : baseDistanceX;
                holdOffset = new Vector3(sideOffset, 0f, inputDirection.y > 0f ? baseDistanceZ : -baseDistanceZ);
            }

            lighter.transform.rotation = Quaternion.Slerp(lighter.transform.rotation, targetRotation, 12 * Time.deltaTime);
        }

        lighter.transform.position = playerPos.position + holdOffset;
    }

    public override void OnEnd()
    {
        ps.Stop();
        PlayerBehaviour.OnPicked -= HandlePicked;
    }
}
