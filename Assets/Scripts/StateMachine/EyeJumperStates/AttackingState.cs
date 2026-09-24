using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AttackingState : BaseState
{
    private StateMachine stateMachine;
    private Rigidbody eyeRb;
    private RangedEnemy eye;
    private Animator eyeAnim;
    private bool hasReachedApex;


    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        eye = gameObject.GetComponent<RangedEnemy>();
        eyeRb = gameObject.GetComponent<Rigidbody>();
        eyeAnim = gameObject.GetComponent<Animator>();

        hasReachedApex = false; // <- adicionar esta linha

        if (stateMachine.HasParam("adaptedStrenght"))
        {
            var _strenght = stateMachine.GetParam<float>("adaptedStrenght");
            JumpTowards(eye.transform.position, eye.jumpHeight, _strenght);
        }

        eye.OnLanded += HandleLanded;
    }
    
    public void JumpTowards(Vector3 eyePosition, float jumpHeight, float forwardForce)
    {
        var playerPosition = eye.Player.transform.position;
        Vector3 direction = playerPosition - eyePosition;
        direction.y = 0f;
        direction.Normalize();

        Vector3 jumpForce = (Vector3.up * jumpHeight) + (direction * forwardForce);

        eyeRb.AddForce(jumpForce, ForceMode.Impulse);
    }

    private void HandleLanded()
    {
        stateMachine.TransitionTo<StunnedState>();
    }

    public override void OnTick()
    {
        if (eyeRb.linearVelocity.y <= 0f)
        {
            hasReachedApex = true;
        }

        if (hasReachedApex && eyeRb.linearVelocity.y < 0f)
        {
            eyeAnim.SetBool("Fall", true);
        }
        
    }
    public override void OnEnd()
    {
        eyeAnim.SetBool("Fall", false);
        eye.OnLanded -= HandleLanded;
    }
}