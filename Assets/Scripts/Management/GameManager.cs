using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    Dialogue dialogue;
    PlayerBehaviour player;

    private List<int> dialogueChoice;
    void Awake()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        
        dialogueChoice = new List<int>();
    }

    void Start()
    {
        dialogueChoice.Add(dialogue.code);
        
        dialogue.gameObject.SetActive(false);
        
        Debug.Log(dialogueChoice);
    }

    void Update()
    {
        
    }
    
    public void ActivateDialogue(int code)
    {
        if (dialogueChoice.Contains(code))
        {
            
        }
        dialogue.gameObject.SetActive(true);
    }
}
