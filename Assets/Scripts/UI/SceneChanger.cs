using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChanger : MonoBehaviour
{
    public GameObject uiImage;
    public GameObject pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        uiImage = GameObject.FindGameObjectWithTag("PauseImg");
        pauseMenu = GameObject.FindGameObjectWithTag("Buttons");
    }

    private void Start()
    {
        if (uiImage != null)
        {
            uiImage.SetActive(false);
        }
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
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

    

    private void MenuSetActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR //Importa funções exclusivas do editor da Unity.
        EditorApplication.isPlaying = false;
#endif
    }
}
