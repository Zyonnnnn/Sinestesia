using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AnimationBehaviour : MonoBehaviour
{
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (videoPlayer.isPaused)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
