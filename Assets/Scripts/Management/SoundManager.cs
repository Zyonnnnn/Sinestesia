using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource music, sfx;
    [SerializeField] AudioClip music1, music2;
    AudioMixer mixer;

    private void Awake()
    {
        mixer = Resources.Load<AudioMixer>("MainMixer");
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "FloorsScene")
        {
            music.clip = music1;
        }
        else if (SceneManager.GetActiveScene().name == "PuzzlesScene")
        {
            music.clip = music2;
        }

        music.Play();
    }

    public void StartMusicAfterScene()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        mixer.SetFloat("Lowpass", 600);
        var lowpass = 600f;

        while (lowpass < 22000f)
        {
            mixer.SetFloat("Lowpass", lowpass);
            lowpass += 500f;
            yield return null;
        }

        mixer.SetFloat("Lowpass", 22000f);
    }
}