using UnityEngine;

public class BossChasingState : BaseState
{
    private MeleeEnemy boss;
    private StateMachine stateMachine;
    private GameObject tentacle;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>().gameObject;
    }

    public override void OnTick()
    {
        if (!boss.Player) return;

        var playerPosition = boss.Player.transform.position;
        var distance = Vector3.Distance(playerPosition, boss.transform.position);
    }
    protected void FollowPlayer(Vector3 playerPosition)
    {
        if (!boss._isTouching)
        {
            tentacle.transform.position = Vector3.MoveTowards(new(tentacle.transform.position.x, 0, tentacle.transform.position.z), playerPosition, boss.GetMoveSpeed() * Time.deltaTime);
        }
    }

    public override void OnEnd()
    {

    }
}
