using UnityEngine;

public class StunnedState : BaseState
{
    private RangedEnemy eye;
    private StateMachine StateMachine;
    public override void OnStart(GameObject gameObject)
    {
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}