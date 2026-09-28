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

        if (eye.isFalling == false)
        {
            eyeAnim.SetTrigger("prepareJump");
        }
    }

    public override void OnTick()
    {
        timer += Time.deltaTime;
        if (timer >= preparingTime)
        {
            if (eye.isFalling == false)
            {
                eyeAnim.SetTrigger("jump");
            }

            stateMachine.TransitionTo<AttackingState>();
            timer = 0;
        }
    }

    public override void OnEnd()
    {
    }
}