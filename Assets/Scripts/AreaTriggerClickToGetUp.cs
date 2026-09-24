using System;
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
                Debug.Log("é pra subir");
                getUp = false;
                player.transform.position = new Vector3(39, 12, 0);
            }
        }
        if (key == 2)
        {
            canGetUp = false;
        }
    }
}