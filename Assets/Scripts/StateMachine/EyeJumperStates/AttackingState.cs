using UnityEngine;

public class AttackingState : BaseState
{
    private StateMachine stateMachine;
    private Rigidbody eyeRb;
    private RangedEnemy eye;
    private Animator eyeAnim;

    ParticleSystem eyePs;
    private bool hasReachedApex;

    private float mass;


    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        eye = gameObject.GetComponent<RangedEnemy>();
        eyeRb = gameObject.GetComponent<Rigidbody>();
        eyeAnim = gameObject.GetComponent<Animator>();
        eyePs = gameObject.GetComponentInChildren<ParticleSystem>();

        hasReachedApex = false;

        // Ensure animator parameters are set so animations play correctly
        if (eyeAnim != null)
        {
            eyeAnim.ResetTrigger("fall");
            eyeAnim.ResetTrigger("prepareJump");
            eyeAnim.SetBool("isWalking", false);
            eyeAnim.SetTrigger("jump");
        }

        // Keep the jump reliable even if the chase state did not set its optional
        // distance-adapted force parameter.
        float forwardForce = stateMachine.GetParam("adaptedStrenght", eye.GetJumpStrenght());
        JumpTowards(eye.transform.position, eye.jumpHeight, forwardForce);

        mass = eyeRb.mass;

        eye.OnLanded += HandleLanded;
    }

    public void JumpTowards(Vector3 eyePosition, float jumpHeight, float forwardForce)
    {
        var playerPosition = eye.Player.transform.position;
        Vector3 direction = playerPosition - eyePosition;
        direction.y = 0f;
        direction.Normalize();

        // Remove residual vertical speed from the previous landing/fall so it
        // cannot cancel the upward impulse at takeoff.
        Vector3 velocity = eyeRb.linearVelocity;
        velocity.y = 0f;
        eyeRb.linearVelocity = velocity;

        Vector3 jumpForce = (Vector3.up * jumpHeight) + (direction * forwardForce);

        // Mark as airborne before applying force so other states/animations know
        eye.isFalling = true;
        eyeRb.AddForce(jumpForce, ForceMode.Impulse);
    }

    private void HandleLanded()
    {
        ScreenShake screenShake = GameObject.FindObjectOfType<ScreenShake>();
        SoundManager audio = GameObject.FindGameObjectWithTag("Music").GetComponent<SoundManager>();

        audio.PlaySFX(audio.eye);
        
        if (screenShake != null)
        {
            screenShake.start = true;
        }

        eyePs.Play();

        if (eye.isFalling == false)
        {
            eyeAnim.SetBool("isWalking", false);
        }

        stateMachine.TransitionTo<StunnedState>();
    }

    public override void OnTick()
    {
        if (eyeRb.linearVelocity.y <= 0f)
        {
            hasReachedApex = true;
        }

        if (hasReachedApex)
        {
            eye.areaDmg.SetActive(true);
            eye.inAttack = true;
            eyeRb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
        }
    }

    public override void OnEnd()
    {
        eyeRb.mass = mass;
        eye.OnLanded -= HandleLanded;
    }
}
