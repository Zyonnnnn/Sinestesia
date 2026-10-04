using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWakeSMB : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = animator.transform.position;
        }
    }
}
