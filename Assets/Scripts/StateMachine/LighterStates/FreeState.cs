using UnityEngine;

public class FreeState : BaseState
{
    LighterBehaviour lighter;
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();
        
        
        lighter.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    public override void OnTick()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
