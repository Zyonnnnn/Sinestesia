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
        uiImage.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiImage.SetActive(!uiImage.activeSelf);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }
}
