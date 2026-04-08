using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] public float attackRange;
    public bool _inAttack;

    private Rigidbody rb;

    private StateMachine StateMachine;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<IdleState>();
    }

    private void Update()
    {
        Debug.Log(StateMachine.CurrentState.ToString());
        StateMachine.OnTick();
        CheckPlayerInRange();
    }
    
    //protected override void Attack()
    //{
    //    _inAttack = true;
    //    var lastPlayerPosition = FindFirstObjectByType<PlayerBehaviour>().transform.position;
    //    StartCoroutine(JumpAttack(lastPlayerPosition));
    //}

    protected override void Attack()
    {
        
    }

    protected override void CheckPlayerInRange()
    {
        var playerPosition = FindFirstObjectByType<PlayerBehaviour>().transform.position;
        var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, transform.position));

        if (distanceFromPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceFromPlayer <= detectRange && distanceFromPlayer > attackRange)
        {
            //FollowPlayer(playerPosition);
            Debug.Log("la ele");
            StateMachine.TransitionTo<ChasingState>();
        }
    }
    
    //IEnumerator JumpAttack(Vector3  lastPlayerPosition)
    //{
    //    yield return new WaitForSeconds(3f);
        
    //    rb.AddForce(new(0, 8, 0), ForceMode.Impulse);

    //    _inAttack = false;
    //}

    public object GetRigidbody() => rb;
}
