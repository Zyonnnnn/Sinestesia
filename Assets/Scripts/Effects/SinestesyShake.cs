using Unity.Cinemachine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class SinestesyShake : MonoBehaviour
{
    public static SinestesyShake instance;
    public float timeToMaxShake;
    public float maxShakeAmount;

    private CinemachinePositionComposer cinemachine;
    private Coroutine shakeCoroutine;
    private Vector3 originalPosition;
    private bool isShaking = false;

    private void Start()
    {
        cinemachine = GetComponent<CinemachinePositionComposer>();
        if (cinemachine != null)
        {
            originalPosition = cinemachine.Composition.ScreenPosition;
        }
    }

    public void StartShake()
    {
        if (!isShaking)
        {
            isShaking = true;
            shakeCoroutine = StartCoroutine(ShakeRoutine());
        }
    }

    public void StopShake()
    {
        if (isShaking)
        {
            isShaking = false;
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
            }
            if (cinemachine != null)
            {
                cinemachine.Composition.ScreenPosition = originalPosition;
            }
        }
    }

    IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (isShaking)
        {
            elapsed += Time.deltaTime;

            float currentStrength = Mathf.Clamp01(elapsed / timeToMaxShake) * maxShakeAmount;

            cinemachine.Composition.ScreenPosition = originalPosition +
                new Vector3(
                    UnityEngine.Random.insideUnitSphere.x,
                    UnityEngine.Random.insideUnitSphere.y,
                    0
                ) * currentStrength;

            yield return null;
        }
    }
}