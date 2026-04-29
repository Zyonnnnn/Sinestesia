using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : BaseState
{
    private StateMachine stateMachine;
    private Rigidbody eyeRb;
    private RangedEnemy eye;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        eye = gameObject.GetComponent<RangedEnemy>();
        eyeRb = gameObject.GetComponent<Rigidbody>();

        if (stateMachine.HasParam("adaptedStrenght"))
        {
            var _strenght = stateMachine.GetParam<float>("adaptedStrenght");
            Debug.Log(_strenght);

            JumpTowards(eye.transform.position, eye.jumpHeight, _strenght);
        }

        eye.OnLanded += HandleLanded;
    }
    public void JumpTowards(Vector3 eyePosition, float jumpHeight, float forwardForce)
    {
        var playerPosition = eye.Player.transform.position;
        Vector3 direction = (playerPosition - eyePosition).normalized;
        direction.y = 0f;

        Vector3 jumpForce = (Vector3.up * jumpHeight) + (direction * forwardForce);

        eyeRb.AddForce(jumpForce, ForceMode.Impulse);
    }

    private void HandleLanded()
    {
        stateMachine.TransitionTo<StunnedState>();
    }

    public override void OnTick()
    {

    }
    public override void OnEnd()
    {
        eye.OnLanded -= HandleLanded;
    }
}