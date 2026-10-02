using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        Debug.Log("sou gay");
    }

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        yield return new WaitForSeconds(1f);
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
