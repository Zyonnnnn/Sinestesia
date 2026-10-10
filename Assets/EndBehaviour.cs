using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Unity.Cinemachine;

public class EndBehaviour : MonoBehaviour, IHitable
{
    [SerializeField] GameObject cinemachine;
    [SerializeField] float vel;
    Vector3 finalPos;

    public bool isEnd;

    private void Awake()
    {
        cinemachine = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        finalPos = new(-0.78f, 3.13f, -28.1f);
    }
    public void Execute(Transform executionSoruce, Rigidbody rb, int key)
    {
        var cinemachineCamera = cinemachine.GetComponent<CinemachineCamera>();
        cinemachineCamera.Follow = null;

        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        var timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;
            cinemachine.transform.position = Vector3.Lerp(cinemachine.transform.position, finalPos, timer);
            yield return null;
        }

        cinemachine.transform.position = finalPos;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("EndScene");
    }
}
