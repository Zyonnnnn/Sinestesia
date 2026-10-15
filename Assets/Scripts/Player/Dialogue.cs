using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    TextMeshProUGUI text;
    InputManager inputManager;

    public Image image;

    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    int index;
    bool canStart;

    void Awake()
    {
        inputManager = new InputManager();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        inputManager.onPassDialoguePressed += HandleDialogue;
        image.enabled = false;

        text.text = string.Empty;
    }

    private void HandleDialogue()
    {
        if (canStart)
        {
            image.enabled = true;
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

    void Update()
    {
        if (canStart)
        {
            image.enabled = true;
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
    }

    public void StartDialogue()
    {
        canStart = true;
        index = 0;

        StartCoroutine(TypeLine());
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
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
            inputManager.onPassDialoguePressed -= HandleDialogue;
            gameObject.SetActive(false);
            Destroy(image.gameObject);
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
