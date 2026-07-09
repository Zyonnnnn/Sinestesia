using UnityEngine;

public class LighterBehaviour : MonoBehaviour, IHitable
{
    StateMachine StateMachine;
    ParticleSystem ps;

    private void Awake()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<FreeState>();

        ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        ps.Stop();
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        StateMachine.TransitionTo<OnHandState>();
        StateMachine.SetParam("PlayerPos", executionSoruce);
        StateMachine.SetParam("PlayerRigidbody", rb);
    }
}