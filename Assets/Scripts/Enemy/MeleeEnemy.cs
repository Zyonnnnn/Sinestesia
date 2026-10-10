using System;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private StateMachine StateMachine;
    EndBehaviour end;
    Animator anim;
    [SerializeField] public static LayerMask layerMask;

    public PlayerBehaviour Player { get; private set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();

        end = FindAnyObjectByType<EndBehaviour>();
        Player = FindFirstObjectByType<PlayerBehaviour>();
    }
    private void Start()
    {
        StateMachine = new StateMachine(this.gameObject);
        StateMachine.TransitionTo<BossIdleState>();
    }

    private void Update()
    {
        if (end.isEnd)
        {
            anim.SetTrigger("End");
        }

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
