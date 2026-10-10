using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public GameObject uiImage, pauseMenu, mMenu, cMenu, deathMenu, playerObj, controlsMenu;
    PlayerBehaviour playerBehaviour;
    InputManager inputManager;
    public Image fadeImg;
    private bool deathMenuShown = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        inputManager = new InputManager();

        inputManager.onPausePressed += HandlePause;

        uiImage = GameObject.FindGameObjectWithTag("PauseImg");
        mMenu = GameObject.FindGameObjectWithTag("MainMenu");
        cMenu = GameObject.FindGameObjectWithTag("ConfigMenu");
        deathMenu = GameObject.FindGameObjectWithTag("DeathM");
        controlsMenu = GameObject.FindGameObjectWithTag("ControlsMenu");
        playerObj = GameObject.FindGameObjectWithTag("Player");

        // inicializa estados seguros se objetos foram encontrados
        if (mMenu != null) mMenu.SetActive(true);
        if (cMenu != null) cMenu.SetActive(false);
        if (uiImage != null) uiImage.SetActive(false);
        if (deathMenu != null) deathMenu.SetActive(false);
        if (controlsMenu != null) controlsMenu.SetActive(false);
    }

    private void HandlePause()
    {
        if(uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);
            if (mMenu != null) mMenu.SetActive(true);
            if (cMenu != null) cMenu.SetActive(false);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }

    private void Start()
    {
        if (playerObj != null)
        {
            playerBehaviour = playerObj.GetComponent<PlayerBehaviour>();
            if (playerBehaviour != null)
                playerBehaviour.OnDie += DeathMenuSetActive;
        }
    }

    private void Update()
    {
        MenuSetActive();
    }

    public static void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ControlsMenuSetActive()
    {
        if (controlsMenu != null) controlsMenu.SetActive(!controlsMenu.activeSelf);
        if (mMenu != null) mMenu.SetActive(!mMenu.activeSelf);
    }


    public void DeathMenuSetActive()
    {
        if (deathMenuShown) return;
        deathMenuShown = true;

        Debug.LogWarning("LIGANDO TELA DE MENU MORTE!");
        if (uiImage != null) uiImage.SetActive(true);
        if (deathMenu != null) deathMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        if (playerBehaviour != null)
            playerBehaviour.OnDie -= DeathMenuSetActive;
    }

    public void MenuSetActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);
            if (mMenu != null) mMenu.SetActive(true);
            if (cMenu != null) cMenu.SetActive(false);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }

    public void ConfigMenu()
    {
        if (mMenu != null && cMenu != null)
        {
            mMenu.SetActive(!mMenu.activeSelf);
            cMenu.SetActive(!cMenu.activeSelf);
        }
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }    
}