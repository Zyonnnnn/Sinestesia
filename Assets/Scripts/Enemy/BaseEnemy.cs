using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int strength;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected float detectRange;
    
    public bool _isTouching;
    
    protected void TakeDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    protected void CheckHealth()
    {
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log("morreo");
        Destroy(gameObject);
    }
    
    protected abstract void Attack();
    protected abstract void CheckPlayerInRange();

    public float GetMoveSpeed() => moveSpeed;
    public float GetDetectRange() => detectRange;
    
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}