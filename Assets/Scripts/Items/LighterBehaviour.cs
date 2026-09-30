using System;
using UnityEngine;

public class LighterBehaviour : MonoBehaviour, IHitable
{
    StateMachine stateMachine;
    ParticleSystem ps;

    ItemsUIBehaviour hud;

    public bool canFire;

    private void Awake()
    {
        stateMachine = new StateMachine(this.gameObject);
        stateMachine.TransitionTo<FreeState>();

        ps = GetComponent<ParticleSystem>();
        hud = GameObject.FindGameObjectWithTag("LighterImg").GetComponent<ItemsUIBehaviour>();
    }

    private void Start()
    {
        ps.Stop();
    }

    private void Update()
    {
        stateMachine.OnTick();

        canFire = stateMachine.CurrentState is OnHandState && stateMachine.GetParam<bool>("canFire");
    }

    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        stateMachine.SetParam("PlayerPos", executionSoruce);
        stateMachine.SetParam("PlayerRigidbody", rb);

        if (stateMachine.CurrentState is OnHandState)
        {
            return;
        }

        stateMachine.TransitionTo<OnHandState>();
    }

    private void OnDestroy()
    {
        Destroy(hud.lighterImage.gameObject);
    }
}
