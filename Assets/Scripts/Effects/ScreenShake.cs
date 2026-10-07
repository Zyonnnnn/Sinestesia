using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] public bool start;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;
    CinemachinePositionComposer cinemachine;

    private void Start()
    {
        cinemachine = GetComponent<CinemachinePositionComposer>();
    }

    private void Update()
    {
        if (start)
        {
            Debug.Log("Screen shake started");
            start = false;
            StartCoroutine(Shake(curve));
        }
    }

    IEnumerator Shake(AnimationCurve curve)
    {
        var startPos = cinemachine.Composition.ScreenPosition;
        var elapsed = 0.0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var strength = curve.Evaluate(elapsed / duration);
            cinemachine.Composition.ScreenPosition = startPos + new Vector2(UnityEngine.Random.insideUnitSphere.x, UnityEngine.Random.insideUnitSphere.y) * strength;
            yield return null;
        }

        cinemachine.Composition.ScreenPosition = startPos;
    }
}
