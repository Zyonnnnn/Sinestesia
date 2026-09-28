using UnityEngine;

class RangedEnemy : BaseEnemy
{
    public float attackRange, jumpHeight, jumpFactor;
    public bool _inAttack;

    private Rigidbody rb;
    private StateMachine StateMachine;

    Animator animator;
    private SpriteRenderer sp;

    public event System.Action OnLanded;
    public bool isFalling;

    public PlayerBehaviour Player { get; private set; }

    protected void Awake()
    {
        Player = FindFirstObjectByType<PlayerBehaviour>();
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<IdleState>();
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.name == "coiso")
            {
                isFalling = true;
                animator.SetTrigger("fall");
            }

            OnLanded?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, null, 0);
        }
    }
}