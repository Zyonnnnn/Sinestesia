using System.Collections;
using System.Linq;
using UnityEngine;

class RangedEnemy : BaseEnemy
{
    public float attackRange, jumpHeight, jumpFactor;
    public bool inAttack;
    private bool flipped;
    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);
    [SerializeField] private float flipSpeed = 10f;

    [SerializeField] private float groundCheckDistance = 0.6f;
    [SerializeField] private LayerMask groundLayer = ~0;
    private bool groundedPreviously = true;

    private Rigidbody rb;
    private StateMachine StateMachine;

    Animator animator;
    private SpriteRenderer sp;
    ParticleSystem ps;

    public GameObject areaDmg;
    [SerializeField] GameObject exp1, exp2;

    public event System.Action OnLanded;
    public bool isFalling;

    public PlayerBehaviour Player { get; private set; }

    protected void Awake()
    {
        Player = FindFirstObjectByType<PlayerBehaviour>();
        areaDmg = transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    protected void Start()
    {
        ps.Stop();

        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<IdleState>();
    }

    private void Update()
    {
        HandleFlip();
        CheckGrounded();
        StateMachine.OnTick();
    }

    private void CheckGrounded()
    {
        // Raycast down to reliably detect ground instead of relying only on collision events
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        bool grounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);

        // isFalling is true when not grounded
        bool nowFalling = !grounded;

        // Started falling this frame
        if (nowFalling && groundedPreviously)
        {
            animator?.SetTrigger("fall");
        }

        // Landed this frame
        if (grounded && groundedPreviously == false)
        {
            isFalling = false;
            // ensure walking animation is reset on land
            animator?.SetBool("isWalking", false);
            // clear jump-related triggers so animator can transition cleanly
            animator?.ResetTrigger("jump");
            animator?.ResetTrigger("prepareJump");
            OnLanded?.Invoke();
        }

        isFalling = nowFalling;
        groundedPreviously = grounded;
    }

    private void HandleFlip()
    {
        if (Player == null) return;

        flipped = Player.transform.position.x > transform.position.x ? false : true;

        var target = flipped ? flipLeft : flipRight;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, flipSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, null, 0);
        }
    }
}