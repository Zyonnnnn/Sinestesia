using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class OnHandState : BaseState
{
    Transform playerPos;
    StateMachine stateMachine;

    InputManager inputManager;
    LighterBehaviour lighter;
    GameObject player;

    ParticleSystem ps;
    SpriteRenderer lighterRenderer;

    ItemsUIBehaviour hud;

    float baseDistanceX = 0.6f, baseDistanceZ = 0.2f;

    bool inArea;

    Vector3 holdOffset;

    private bool canFire, toDrop;
    public bool isFired;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        Debug.Log("OnHandState started");
        this.stateMachine = stateMachine;

        lighter = gameObject.GetComponent<LighterBehaviour>();
        ps = gameObject.GetComponent<ParticleSystem>();
        lighterRenderer = gameObject.GetComponent<SpriteRenderer>();

        hud = GameObject.FindGameObjectWithTag("LighterImg").GetComponent<ItemsUIBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player");

        inputManager = new InputManager();
        holdOffset = new Vector3(baseDistanceX, 0f, 0f);

        PlayerBehaviour.OnPicked += HandlePicked;

        lighterRenderer.enabled = false;
        hud.lighterImage.enabled = true;

        Wait();
    }


    public override void OnTick()
    {
        stateMachine.SetParam("canFire", canFire);

        inArea = PlayerBehaviour.canInteract;

        HandlePosAndRot();
    }

    public override void OnEnd()
    {
        hud.lighterImage.enabled = false;
        lighterRenderer.enabled = true;

        ps.Stop();

        PlayerBehaviour.OnPicked -= HandlePicked;
    }

    void HandlePosAndRot()
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

            lighter.transform.rotation =
                Quaternion.Slerp(lighter.transform.rotation, targetRotation, 12 * Time.deltaTime);
        }

        lighter.transform.position = playerPos.position + holdOffset;
    }

    private void HandlePicked()
    {
        if (inArea)
        {
            FireCoroutine(2);

            if (ps != null)
            {
                if (!ps.isEmitting)
                {
                    isFired = true;
                    ps.Play();
                }
            }
        }
        else
        {
            if (toDrop)
            {
                toDrop = false;
                stateMachine.TransitionTo<FreeState>();
            }
        }
    }

    private async void FireCoroutine(float t)
    {
        player.GetComponent<Animator>().SetTrigger("Interact");
        canFire = true;
        lighter.gameObject.GetComponent<BoxCollider>().enabled = true;
        lighterRenderer.enabled = true;

        await Task.Delay(TimeSpan.FromSeconds(t));

        canFire = false;
        lighter.gameObject.GetComponent<BoxCollider>().enabled = false;
        lighterRenderer.enabled = false;
    }

    private async void Wait()
    {
        await Task.Delay(TimeSpan.FromSeconds(0.5f));
        toDrop = true;
    }
}