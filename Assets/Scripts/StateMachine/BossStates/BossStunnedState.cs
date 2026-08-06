using UnityEngine;

public class BossStunnedState : BaseState
{
    private MeleeEnemy boss;
    TentacleBehaviour tentacle;
    
    private StateMachine stateMachine;

    private float timer;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        
        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
        
        timer = 3;

        Debug.Log("Boss is stunned!");
    }

    public override void OnTick()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Comeback();
        }
    }

    void Comeback()
    {
        tentacle.transform.position = Vector3.Slerp(tentacle.transform.position, stateMachine.GetParam<Vector3>("tentaclePosition"), 25f * Time.deltaTime);
        
        if (tentacle.transform.position == stateMachine.GetParam<Vector3>("tentaclePosition"))
        {
            stateMachine.TransitionTo<BossIdleState>();
        }
    }

    public override void OnEnd()
    {

    }
}
