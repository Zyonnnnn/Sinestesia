using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        Debug.Log("sou hetero");
    }

    void Start()
    {
        StartCoroutine(FadeInRoutine());
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    private IEnumerator FadeInRoutine()
    {
        var timer = 1f;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0f, 1f, timer));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        this.gameObject.SetActive(false);
    }
}
