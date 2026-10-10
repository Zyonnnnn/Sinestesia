using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool onSinestesy, isFading;

    Dialogue dialogue;
    GameObject playerBehaviour;
    SinestesyDetection sinestesyDetection;

    InputManager inputManager;

    [Header("Time Stats")] [SerializeField]
    private float sinestesyFadeOutTime = 0.5f;

    [Header("References")] [SerializeField]
    private ScriptableRendererFeature sinestesyEffect;

    [SerializeField] private Material material;

    [Header("Intensity Stats")] [SerializeField]
    private float voronoiIntensityStat;

    [SerializeField] private float vignetteIntensityStat;

    private int voronoiIntensity = Shader.PropertyToID("VoronoiPower");
    private int vignetteIntensity = Shader.PropertyToID("VignetteIntensity");

    void Awake()
    {
        GameObject dialogueObject = GameObject.Find("Dialogue");
        if (dialogueObject != null)
        {
            dialogue = dialogueObject.GetComponentInChildren<Dialogue>();
        }

        playerBehaviour = GameObject.Find("Player");
        if (playerBehaviour != null)
        {
            sinestesyDetection = playerBehaviour.GetComponentInChildren<SinestesyDetection>();
        }

        inputManager = new InputManager();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FloorsScene")
        {
            if (playerBehaviour != null)
            {
                playerBehaviour.SetActive(false);
            }
        }

        if (sinestesyEffect != null)
        {
            sinestesyEffect.SetActive(false);
        }

        if (inputManager != null)
        {
            inputManager.OnSinestesyPressed += HandleSinestesy;
        }
    }

    private void HandleSinestesy()
    {
        if (sinestesyDetection != null && sinestesyDetection.isSinestesiaActive)
        {
            if (!isFading)
            {
                StartCoroutine(SinestesyRoutine());
            }
        }
    }

    private IEnumerator SinestesyRoutine()
    {
        if (sinestesyEffect == null || material == null)
        {
            yield break;
        }

        isFading = true;
        sinestesyEffect.SetActive(true);

        float elapsedTime = 0f;

        if (!onSinestesy)
        {
            while (elapsedTime < sinestesyFadeOutTime)
            {
                elapsedTime += Time.deltaTime;

                float lerpedVoronoi = Mathf.Lerp(0f, voronoiIntensityStat, elapsedTime / sinestesyFadeOutTime);
                float lerpedVignette = Mathf.Lerp(0f, vignetteIntensityStat, elapsedTime / sinestesyFadeOutTime);

                material.SetFloat(voronoiIntensity, lerpedVoronoi);
                material.SetFloat(vignetteIntensity, lerpedVignette);

                yield return null;
            }

            material.SetFloat(voronoiIntensity, voronoiIntensityStat);
            material.SetFloat(vignetteIntensity, vignetteIntensityStat);
        }
        else
        {
            while (elapsedTime < sinestesyFadeOutTime)
            {
                elapsedTime += Time.deltaTime;

                float lerpedVoronoi = Mathf.Lerp(voronoiIntensityStat, 0f, elapsedTime / sinestesyFadeOutTime);
                float lerpedVignette = Mathf.Lerp(vignetteIntensityStat, 0f, elapsedTime / sinestesyFadeOutTime);

                material.SetFloat(voronoiIntensity, lerpedVoronoi);
                material.SetFloat(vignetteIntensity, lerpedVignette);

                yield return null;
            }

            material.SetFloat(voronoiIntensity, 0f);
            material.SetFloat(vignetteIntensity, 0f);

            sinestesyEffect.SetActive(false);
        }

        onSinestesy = !onSinestesy;
        isFading = false;
    }

    public void ActivateDialogue()
    {
        if (dialogue != null)
        {
            dialogue.StartDialogue();
        }
    }

    public void DeactivateDialogue()
    {
        if (dialogue != null)
        {
            dialogue.StopDialogue();
        }
    }

    public void WakePlayer(Animator animator)
    {
        if (playerBehaviour != null && animator != null)
        {
            playerBehaviour.SetActive(true);
            playerBehaviour.transform.position = animator.transform.position;
        }
    }
}