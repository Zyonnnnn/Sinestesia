using System;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    
    private void Update()
    {
        CheckPlayerInRange();
    }

    protected override void CheckPlayerInRange()
    {
        var playerPosition = FindFirstObjectByType<PlayerBehaviour>().transform.position;
        var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, transform.position));

        if (distanceFromPlayer <= detectRange)
        {
            FollowPlayer(playerPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isTouching = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isTouching = false;
        }
    }
    
    protected override void Attack()
    {
        
    }
}
