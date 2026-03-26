using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] private float attackRange;
    private bool _inAttack;

    private Rigidbody rb;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_inAttack)
        {
            CheckPlayerInRange();
        }
    }
    
    protected override void Attack()
    {
        _inAttack = true;
        var lastPlayerPosition = FindFirstObjectByType<PlayerBehaviour>().transform.position;
        StartCoroutine(JumpAttack(lastPlayerPosition));
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
            FollowPlayer(playerPosition);
        }
    }

    IEnumerator JumpAttack(Vector3  lastPlayerPosition)
    {
        yield return new WaitForSeconds(3f);
        
        rb.AddForce(new(0, 8, 0), ForceMode.Impulse);

        _inAttack = false;
    }
}
