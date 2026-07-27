using UnityEngine;

public class BossChasingState : BaseState
{
    private MeleeEnemy boss;
    private TentacleBehaviour tentacle;

    private StateMachine stateMachine;

    private float timer = 3f;

    public static bool isMoving;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        boss = gameObject.GetComponent<MeleeEnemy>();
        tentacle = gameObject.GetComponentInChildren<TentacleBehaviour>();
    }

    public override void OnTick()
    {
        if (!boss.Player) return;

        var playerPosition = boss.Player.transform.position;
        var distance = Vector3.Distance(playerPosition, boss.transform.position);

        if (distance >= boss.GetDetectRange())
        {
            stateMachine.TransitionTo<BossIdleState>();
            isMoving = false;
        }
        else
        {
            FollowPlayer(playerPosition);
            isMoving = true;
        }
    }

    protected void FollowPlayer(Vector3 playerPosition)
    {
        tentacle.transform.position = Vector3.MoveTowards(tentacle.transform.position, new Vector3(playerPosition.x, tentacle.transform.position.y, playerPosition.z), boss.GetMoveSpeed() * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.TransitionTo<BossPreparingAttack>();
        }
    }

    public override void OnEnd()
    {

    }
}
