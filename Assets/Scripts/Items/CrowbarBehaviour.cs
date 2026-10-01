using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowbarBehaviour : MonoBehaviour
{
    PlayerBehaviour playerBehaviour;
    InputManager inputManager;
    Image hud;
    SpriteRenderer sp;

    private bool isPicked;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        
    }
}