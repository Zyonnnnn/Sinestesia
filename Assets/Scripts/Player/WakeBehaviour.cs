using UnityEngine;

public class WakeBehaviour : MonoBehaviour
{
    Animator animator;
    GameObject player;
    CutSceneBehaviour cutSceneBehaviour;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cutSceneBehaviour = GameObject.Find("CutScene").GetComponent<CutSceneBehaviour>();
    }

    private void Update()
    {
        if (cutSceneBehaviour.cutsceneFinished)
        {
            animator.SetTrigger("Wake");
        }
    }

    public void Wake(int i)
    {
        if (i == 1)
        {
            player.SetActive(true);
            player.transform.position = transform.position + Vector3.up * 2f;

            Destroy(gameObject);
        }
    }
}
