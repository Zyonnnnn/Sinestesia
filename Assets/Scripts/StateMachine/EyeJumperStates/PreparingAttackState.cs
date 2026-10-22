using UnityEngine;

public class PreparingAttackState : BaseState
{
    private readonly float preparingTime = 1;
    private float timer;

    private RangedEnemy eye;
    private StateMachine stateMachine;
    private Animator eyeAnim;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        eye = gameObject.GetComponent<RangedEnemy>();
        eyeAnim = eye.GetComponent<Animator>();
        timer = 0f;

        if (!eye.isFalling)
        {
            eyeAnim.SetTrigger("prepareJump");
        }
    }

    public override void OnTick()
    {
        timer += Time.deltaTime;
        if (timer >= preparingTime)
        {
            stateMachine.TransitionTo<AttackingState>();
        }
    }

    public override void OnEnd()
    {
    }
}
