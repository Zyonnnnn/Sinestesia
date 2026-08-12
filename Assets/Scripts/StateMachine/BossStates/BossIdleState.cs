using UnityEngine;

public class BossIdleState : BaseState
{
    private MeleeEnemy boss;
    private TentacleBehaviour tentacle;
    
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        
        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
        
        stateMachine.SetParam("tentaclePosition", tentacle.transform.position);
        stateMachine.SetParam("velocity", boss.GetStrength());
    }

    public override void OnTick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            stateMachine.TransitionTo<BossChasingState>();
        }
    }

    public override void OnEnd()
    {

    }
}
