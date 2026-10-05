using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    Image image;
    bool finish = false;

    CutSceneBehaviour cutSceneBehaviour;

    void Awake()
    {
        image = GetComponent<Image>();
        cutSceneBehaviour = GameObject.Find("CutScene").GetComponent<CutSceneBehaviour>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "FloorsScene")
        {
            if (cutSceneBehaviour != null)
            {
                if (!finish)
                {
                    if (cutSceneBehaviour.cutsceneFinished)
                    {
                        StartCoroutine(FadeInRoutine());
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "PuzzlesScene")
        {
            StartCoroutine(OtherFadeInRoutine());
        }
    }

    private IEnumerator FadeInRoutine()
    {
        finish = true;
        yield return new WaitForSeconds(3f);
        var timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1f, 0f, timer));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
    IEnumerator OtherFadeInRoutine()
    {
        var timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1f, 0f, timer));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
}
