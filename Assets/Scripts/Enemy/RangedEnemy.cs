using UnityEngine;

class RangedEnemy : BaseEnemy
{
    public float attackRange, jumpHeight, jumpFactor;
    public bool inAttack;
    private bool flipped;
    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);
    [SerializeField] private float flipSpeed = 10f;

    private Rigidbody rb;
    private StateMachine StateMachine;

    Animator animator;
    private SpriteRenderer sp;
    ParticleSystem ps;

    public GameObject areaDmg;

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
        StateMachine.OnTick();
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