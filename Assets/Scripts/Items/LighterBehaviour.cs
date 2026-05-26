using UnityEngine;

public class LighterBehaviour : MonoBehaviour, IHitable
{
    StateMachine StateMachine;

    private void Awake()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<FreeState>();
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    public void Execute(Transform executionSoruce, Rigidbody rb)
    {
        StateMachine.TransitionTo<OnHandState>();
        StateMachine.SetParam("PlayerPos", executionSoruce);
        StateMachine.SetParam("PlayerRigidbody", rb);
    }
}