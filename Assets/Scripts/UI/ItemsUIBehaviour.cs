using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUIBehaviour : MonoBehaviour
{
    public Image lighterImage;
    private void Awake()
    {
        lighterImage = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        lighterImage.enabled = false;
    }
}
