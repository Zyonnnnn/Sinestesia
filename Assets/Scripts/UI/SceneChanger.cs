using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject uiImage;
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        uiImage = GameObject.FindGameObjectWithTag("PauseImg");
    }

    private void Start()
    {
        if (uiImage != null)
        {
            uiImage.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }
}
