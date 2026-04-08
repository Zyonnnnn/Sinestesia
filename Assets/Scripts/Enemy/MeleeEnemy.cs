using System;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private StateMachine StateMachine;

    private void Start()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<IdleState>();
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    //protected override void CheckPlayerInRange()
    //{
    //    var playerPosition = FindFirstObjectByType<PlayerBehaviour>().transform.position;
     //   var distanceFromPlayer = Mathf.Abs( Vector3.Distance(playerPosition, transform.position));
//
     //   if (distanceFromPlayer <= detectRange)
      //  {
      //      FollowPlayer(playerPosition);
    //    }
    //}

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

    protected override void CheckPlayerInRange()
    {
        
    }
}
