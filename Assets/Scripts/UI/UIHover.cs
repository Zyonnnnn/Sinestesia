using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour
{
    [Header("Configuração do Outline")]
    public float outlineNormal = 0f;
    public float outlineHover = 0.2f;

    private TMP_Text texto;

    void Awake()
    {
        texto = GetComponent<TMP_Text>();

        // Define o outline inicial
        texto.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineNormal);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, outlineNormal);
    }
}
