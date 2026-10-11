using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTriggerClickToGetUp : MonoBehaviour, IHitable
{
    GameObject player;
    GameManager gameManager;
    InputManager inputManager;

    private bool getUp, canGetUp, gettingUp;

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
            if (getUp && !gettingUp)
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
        gettingUp = true;

        var animator = player.GetComponent<Animator>();
        var rb = player.GetComponent<Rigidbody>();
        var playerBehaviour = player.GetComponent<PlayerBehaviour>();

        animator.SetTrigger("Climb");
        rb.isKinematic = true;
        playerBehaviour.HandleStop();


        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(player.transform.position, new Vector3(35.3f, player.transform.position.y, -0.3f)) > 0.1f)
        {
            animator.SetBool("Walk", true);
            Vector3 newPosition = Vector3.MoveTowards(player.transform.position, new Vector3(35.3f, player.transform.position.y, -0.3f), Time.deltaTime * 5);
            player.transform.position = newPosition;
            yield return null;
        }

        player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 12 * Time.deltaTime);

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
        gettingUp = false;
    }
}