using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChanger : MonoBehaviour
{
    [Header("Painéis (arraste no Inspector; se vazio, busca pela tag)")]
    public GameObject uiImage;      // raiz do menu de pause (tag PauseImg)
    public GameObject mMenu;        // [Panel] MainMenu / Initial
    public GameObject cMenu;        // [Panel] Config
    public GameObject controlsMenu; // [Panel] Controls
    public GameObject deathMenu;    // [UI] Morte
    public GameObject playerObj;
    public Image fadeImg;

    private PlayerBehaviour playerBehaviour;
    private int lastPauseFrame = -1;
    private bool deathMenuShown = false;

    private void Awake()
    {
        Time.timeScale = 1f;

        // FindGameObjectWithTag NÃO acha objetos desativados, então busco incluindo inativos.
        if (uiImage == null)      uiImage      = FindByTag("PauseImg");
        // Painéis do menu de pause: procuro pelo NOME dentro do menu (as tags se repetem).
        if (mMenu == null)        mMenu        = FindChildByName(uiImage, "[Panel] MainMenu") ?? FindByTag("MainMenu");
        if (cMenu == null)        cMenu        = FindChildByName(uiImage, "[Panel] Config")   ?? FindByTag("ConfigMenu");
        if (controlsMenu == null) controlsMenu = FindByTag("ControlsMenu", false);
        if (deathMenu == null)    deathMenu    = FindByTag("DeathM", false) ?? FindChildByName(uiImage, "[Panel] Death");
        if (playerObj == null)    playerObj    = GameObject.FindGameObjectWithTag("Player");

        EnsureEventSystem();
    }

    private void Start()
    {
        if (playerObj != null)
        {
            playerBehaviour = playerObj.GetComponent<PlayerBehaviour>();
            if (playerBehaviour != null)
                if (playerBehaviour != null) playerBehaviour.OnDie += DeathMenuSetActive;
        }

        ResetMenus();
    }

    private void OnDestroy()
    {
        if (playerBehaviour != null) playerBehaviour.OnDie -= DeathMenuSetActive;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Esc pelo Input legado (o projeto está em "Both"). Não depende do InputManager.
        if (Input.GetKeyDown(KeyCode.Escape)) HandlePause();
    }

    // ---------- Pause ----------

    private void HandlePause()
    {
        // Garante no máximo uma troca por frame.
        if (Time.frameCount == lastPauseFrame) return;
        lastPauseFrame = Time.frameCount;

        if (uiImage == null) return;
        if (deathMenu != null && deathMenu.activeSelf) return; // não pausa na tela de morte

        bool open = !uiImage.activeSelf;
        uiImage.SetActive(open);

        HidePanels();
        SetActive(mMenu, true);

        Time.timeScale = open ? 0f : 1f;
    }

    private void ResetMenus()
    {
        HidePanels();
        SetActive(mMenu, true);
        SetActive(uiImage, false);
    }

    // Desliga todos os "[Panel] ..." do menu de pause (inclusive o de morte, que antes ficava
    // sempre ligado por cima do menu principal e roubava os cliques dos outros botões).
    private void HidePanels()
    {
        if (uiImage != null)
            foreach (var t in uiImage.GetComponentsInChildren<Transform>(true))
                if (t.name.StartsWith("[Panel]")) t.gameObject.SetActive(false);

        SetActive(mMenu, false);
        SetActive(cMenu, false);
        SetActive(controlsMenu, false);
        SetActive(deathMenu, false);
    }

    // ---------- Botões (use estes no OnClick) ----------

    // Método de instância: aparece normalmente no OnClick. Troque o antigo estático por este.
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    // Mantido para o AreaTriggerSceneChanger, que chama SceneChanger.SceneChange(...)
    public static void SceneChange(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        if (uiImage != null && uiImage.activeSelf) HandlePause();
        if (controlsMenu != null) controlsMenu.SetActive(!controlsMenu.activeSelf);
        if (mMenu != null) mMenu.SetActive(!mMenu.activeSelf);
    }


    public void ConfigMenu()
    {
        if (mMenu == null || cMenu == null) return;
        bool showConfig = !cMenu.activeSelf;
        cMenu.SetActive(showConfig);
        mMenu.SetActive(!showConfig);
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

    public void ControlsMenuSetActive()
    {
        if (mMenu == null || controlsMenu == null) return;
        bool showControls = !controlsMenu.activeSelf;
        controlsMenu.SetActive(showControls);
        mMenu.SetActive(!showControls);
        if (Input.GetKeyDown(KeyCode.Escape) && uiImage != null)
        {
            uiImage.SetActive(!uiImage.activeSelf);
            if (mMenu != null) mMenu.SetActive(true);
            if (cMenu != null) cMenu.SetActive(false);

            Time.timeScale = uiImage.activeSelf ? 0f : 1f;
        }
    }

    public void DeathMenuSetActive()
    {
        HidePanels();
        SetActive(uiImage, true);
        SetActive(deathMenu, true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    // ---------- Helpers ----------

    private static void SetActive(GameObject go, bool value)
    {
        if (go != null) go.SetActive(value);
    }

    private static GameObject FindByTag(string tag, bool warn = true)
    {
        foreach (var t in FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            if (t.CompareTag(tag)) return t.gameObject;
        if (warn) Debug.LogWarning($"[SceneChanger] Nenhum objeto com a tag '{tag}' nesta cena.");
        return null;
    }

    private static GameObject FindChildByName(GameObject root, string objName)
    {
        if (root == null) return null;
        foreach (var t in root.GetComponentsInChildren<Transform>(true))
            if (t.name == objName) return t.gameObject;
        return null;
    }

    // Sem EventSystem + módulo de input, NENHUM botão recebe clique.
    private static void EnsureEventSystem()
    {
        var es = FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
        if (es == null)
        {
            es = new GameObject("EventSystem").AddComponent<UnityEngine.EventSystems.EventSystem>();
            Debug.LogWarning("[SceneChanger] Não havia EventSystem na cena; criei um.");
        }

        var legacy = es.GetComponent<UnityEngine.EventSystems.BaseInputModule>();
        if (legacy == null)
        {
#if ENABLE_INPUT_SYSTEM
            es.gameObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
#else
            es.gameObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
#endif
        }
    }
}
