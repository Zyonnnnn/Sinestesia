using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    [Header("Time Stats")]
    [SerializeField] private float sinestesyFadeOutTime = 0.5f;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature sinestesyEffect;
    [SerializeField] private Material material;

    [Header("Intensity Stats")]
    [SerializeField] private float voronoiIntensityStat;
    [SerializeField] private float vignetteIntensityStat;

    private int voronoiIntensity = Shader.PropertyToID("VoronoiPower");
    private int vignetteIntensity = Shader.PropertyToID("VignetteIntensity");

    void Awake()
    {
        //dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
        playerBehaviour = GameObject.Find("Player");
        sinestesyDetection = playerBehaviour.GetComponentInChildren<SinestesyDetection>();

        inputManager = new InputManager();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FloorsScene")
        {
            playerBehaviour.SetActive(false);
        }
        //dialogue.gameObject.SetActive(false);
        sinestesyEffect.SetActive(false);

        inputManager.OnSinestesyPressed += HandleSinestesy;
    }

    private void HandleSinestesy()
    {
        if (sinestesyDetection.isSinestesiaActive)
        {
            if (!isFading)
            {
                StartCoroutine(SinestesyRoutine());
            }
        }
    }
    private IEnumerator SinestesyRoutine()
    {
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
        dialogue.StartDialogue();
    }

    public void DeactivateDialogue()
    {
        dialogue.StopDialogue();
    }

    public void WakePlayer(Animator animator)
    {
        playerBehaviour.SetActive(true);
        playerBehaviour.transform.position = animator.transform.position;
    }
}
