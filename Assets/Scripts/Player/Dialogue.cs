using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    TextMeshProUGUI text;
    
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    
    int index;

    PlayerBehaviour playerBehaviour;
    
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void Start()
    {
        text.text = string.Empty;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerBehaviour.textToFecart)
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
