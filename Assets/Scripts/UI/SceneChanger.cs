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
    public GameObject uiImage, pauseMenu, mMenu, cMenu, deathMenu, playerObj;
    PlayerBehaviour playerBehaviour;
    InputManager inputManager;
    public Image fadeImg;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        inputManager = new InputManager();

        inputManager.onPausePressed += HandlePause;

        uiImage = GameObject.FindGameObjectWithTag("PauseImg");
        mMenu = GameObject.FindGameObjectWithTag("MainMenu");
        cMenu = GameObject.FindGameObjectWithTag("ConfigMenu");
        deathMenu = GameObject.FindGameObjectWithTag("DeathM");
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void HandlePause()
    {
        if(uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);
            mMenu.SetActive(true);
            cMenu.SetActive(false);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }

    private void Start()
    {
        if (playerObj != null)
        {
            playerBehaviour = playerObj.GetComponent<PlayerBehaviour>();
            playerBehaviour.OnDie += DeathMenuSetActive;
        }

        mMenu.SetActive(true);
        cMenu.SetActive(false);
        uiImage.SetActive(false);
        deathMenu.SetActive(false);
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

    private void DeathMenuSetActive()
    {
        Debug.LogWarning("LIGANDO TELA DE MENU MORTE!");
        deathMenu.SetActive(true);
    }

    public void MenuSetActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);
            mMenu.SetActive(true);
            cMenu.SetActive(false);

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