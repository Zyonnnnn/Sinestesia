using System;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private StateMachine StateMachine;


    public PlayerBehaviour Player { get; private set; }

    private void Awake()
    {
        Player = FindFirstObjectByType<PlayerBehaviour>();
    }
    private void Start()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<BossIdleState>();
    }

    private void Update()
    {
        StateMachine.OnTick();
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
}
