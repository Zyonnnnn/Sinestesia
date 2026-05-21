using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR //Importa funções exclusivas do editor da Unity.
        EditorApplication.isPlaying = false;
#endif
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
