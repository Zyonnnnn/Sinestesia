using System;
using System.Collections;
using UnityEngine;
using TMPro;
using EasyTextEffects;

public class Dialogue : MonoBehaviour
{
    TextMeshProUGUI text;
    TextEffect effects;
    
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    
    int index;
    
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        effects = GetComponent<TextEffect>();
    }

    private void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

    void StartDialogue()
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
