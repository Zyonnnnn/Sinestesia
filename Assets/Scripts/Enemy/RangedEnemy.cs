using UnityEngine;

class RangedEnemy : BaseEnemy
{
    [SerializeField] public float attackRange;
    public bool _inAttack;

    private Rigidbody rb;
    private StateMachine StateMachine;

    public event System.Action OnLanded;
    
    public PlayerBehaviour Player { get; private set; }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        Player = FindFirstObjectByType<PlayerBehaviour>();
        
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<IdleState>();
    }

    private void Update()
    {
        Debug.Log(StateMachine.CurrentState.ToString());
        StateMachine.OnTick();
        CheckPlayerInRange();
    }
    
    protected override void Attack()
    {
        
    }

    protected override void CheckPlayerInRange()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnLanded?.Invoke();
        }
    }
}