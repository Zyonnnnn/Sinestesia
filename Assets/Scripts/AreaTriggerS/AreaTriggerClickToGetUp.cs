using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTriggerClickToGetUp : MonoBehaviour, IHitable
{
    GameObject player;
    GameManager gameManager;
    InputManager inputManager;

    private bool getUp;
    bool canGetUp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        inputManager = new InputManager();
    }

    private void Start()
    {
        inputManager.OnPickPressed += HandleUp;
    }

    private void HandleUp()
    {
        if (canGetUp)
        {
            getUp = true;
        }
    }

    public void Execute(Transform executionSoruce, Rigidbody rb, int key)
    {
        if (key == 1)
        {
            canGetUp = true;
            if (getUp)
            {
                getUp = false;
                StartCoroutine(GetUpAnimation());
            }
        }
        if (key == 2)
        {
            canGetUp = false;
        }
    }

    private IEnumerator GetUpAnimation()
    {
        var animator = player.GetComponent<Animator>();
        var rb = player.GetComponent<Rigidbody>();
        var playerBehaviour = player.GetComponent<PlayerBehaviour>();

        animator.SetTrigger("Climb");
        rb.isKinematic = true;
        playerBehaviour.HandleStop();

        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(player.transform.position, new Vector3(48.5f, 11.75f, 0)) > 0.1f)
        {
            animator.SetBool("Walk", true);
            Vector3 newPosition = Vector3.MoveTowards(player.transform.position, new Vector3(48.5f, 11.75f, 0), Time.deltaTime * 5);
            player.transform.position = newPosition;
            yield return null;
        }

        animator.SetBool("Walk", false);
        playerBehaviour.HandleStop();

        rb.isKinematic = false;
        player.transform.position = new Vector3(48.5f, 11.75f, 0);
    }
}