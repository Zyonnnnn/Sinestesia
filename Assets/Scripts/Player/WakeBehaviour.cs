using UnityEngine;

public class WakeBehaviour : MonoBehaviour
{
    Animator animator;
    GameObject player;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Wake(int i)
    {
        if (i == 1)
        {
            player.SetActive(true);
            player.transform.position = transform.position;

            Destroy(this.gameObject);
        }
    }
}
