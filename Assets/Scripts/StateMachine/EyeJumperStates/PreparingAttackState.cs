using UnityEngine;

public class PreparingAttackState : BaseState
{
    private RangedEnemy eye;
    private StateMachine StateMachine;
public override void OnStart(GameObject gameObject)
{
    eye = gameObject.GetComponent<RangedEnemy>();
}

public override void OnTick()
{
    eye._inAttack = true;
    var lastPlayerPosition = Object.FindFirstObjectByType<PlayerBehaviour>().transform.position;
    //Object.StartCoroutine(JumpAttack(lastPlayerPosition));
}

public override void OnEnd()
{
        
}
}