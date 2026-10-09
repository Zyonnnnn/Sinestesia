using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneBehaviour : MonoBehaviour
{
    Animator animator;
    Image image;
    InputManager inputManager;

    int i = 0;

    public bool cutsceneFinished = false;
    private void Awake()
    {
        inputManager = new InputManager();
    }
    void Start()
    {
        inputManager.onPassDialoguePressed += HandlePassDialogue;

        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    private void HandlePassDialogue()
    {
        PassAnimation();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PassAnimation();
        }
    }

    void PassAnimation()
    {
        if (i == 0)
        {
            animator.SetTrigger("Next0");
            i++;
        }
        else if (i == 1)
        {
            animator.SetTrigger("Next1");
            i++;
        }
        else if (i == 2)
        {
            animator.SetTrigger("Next2");
            i++;
            cutsceneFinished = true;
            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2f);

        var timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1f, 0f, timer));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        Destroy(gameObject);
    }
}
