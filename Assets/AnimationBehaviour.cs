using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimationBehaviour : MonoBehaviour
{
    VideoPlayer videoPlayer;
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        StartCoroutine(PlayDelay());
    }

    void Update()
    {
        if (videoPlayer.isPaused)
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(3f);
        videoPlayer.Play();
    }
}
