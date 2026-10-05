using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWakeSMB : StateMachineBehaviour
{
    GameObject gameManager;
    void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager.GetComponent<GameManager>().WakePlayer(animator);
        Destroy(animator.gameObject);
    }
}
