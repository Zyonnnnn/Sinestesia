using System.Collections;
using UnityEngine;

public class AttackingState : BaseState
{
    private RangedEnemy eye;
    private StateMachine StateMachine;
    public override void OnStart(GameObject gameObject)
    {
        eye = gameObject.GetComponent<RangedEnemy>();
    }

    public override void OnTick()
    {
        IEnumerator JumpAttack(Vector3  lastPlayerPosition)
        {
            yield return new WaitForSeconds(3f);
        
            //objectRb.AddForce(new(0, 8, 0), ForceMode.Impulse);

            eye._inAttack = false;
        }
    }

    public override void OnEnd()
    {
        
    }
}