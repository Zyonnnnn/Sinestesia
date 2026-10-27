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
    public AudioClip crack, gas, explosion, eye, sniff, squish;
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
            mixer.SetFloat("Lowpass", 300);
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

    public void PlaySFX(AudioClip audioClip)
    {
        sfx.PlayOneShot(audioClip);
    }

    private IEnumerator FadeIn()
    {
        var lowpass = 300f;

        while (lowpass < 22000f)
        {
            mixer.SetFloat("Lowpass", lowpass);
            lowpass += 125;
            yield return null;
        }

        mixer.SetFloat("Lowpass", 22000f);
    }
}