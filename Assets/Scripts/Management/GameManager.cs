using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    Dialogue dialogue;
    PlayerBehaviour player;

    void Awake()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    void Start()
    {
        dialogue.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    
    public void ActivateDialogue()
    {
        dialogue.gameObject.SetActive(true);
    }
}
