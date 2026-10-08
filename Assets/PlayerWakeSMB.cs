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
        if (animator == null)
        {
            return;
        }

        if (gameManager != null)
        {
            GameManager manager = gameManager.GetComponent<GameManager>();
            if (manager != null)
            {
                manager.WakePlayer(animator);
            }
        }

        Destroy(animator.gameObject);
    }
}
