using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChanger : MonoBehaviour
{
    public GameObject uiImage;
    public GameObject pauseMenu;
    public GameObject mMenu;
    public GameObject cMenu;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        uiImage = GameObject.FindGameObjectWithTag("PauseImg");
        mMenu = GameObject.FindGameObjectWithTag("MainMenu");
        cMenu = GameObject.FindGameObjectWithTag("ConfigMenu");
    }

    private void Start()
    {
        if (mMenu != null && cMenu != null)
        {
            mMenu.SetActive(true);
            cMenu.SetActive(false);
        }
        
        if (uiImage != null)
        {
            uiImage.SetActive(false);
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

#if UNITY_EDITOR //Importa funções exclusivas do editor da Unity.
        EditorApplication.isPlaying = false;
#endif
    }
}
