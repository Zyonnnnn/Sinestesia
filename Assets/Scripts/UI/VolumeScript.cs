using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] private string mixerResourceName = "MainMixer";
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string parameterName = "MasterVolume";
 
    private AudioMixer audioMixer;
 
    private void Start()
    {
        // Busca o AudioMixer direto da pasta Resources, sem precisar arrastar no Inspector
        audioMixer = Resources.Load<AudioMixer>(mixerResourceName);
 
        if (audioMixer == null)
        {
            Debug.LogError($"AudioMixer '{mixerResourceName}' não encontrado em uma pasta Resources.");
            return;
        }
 
        // Carrega o volume salvo (ou 0 dB, volume normal, se não houver nada salvo)
        float savedVolume = PlayerPrefs.GetFloat(parameterName, 0f);
        audioMixer.SetFloat(parameterName, savedVolume);
 
        if (volumeSlider != null)
            volumeSlider.value = savedVolume;
    }
 
    public void SetVolume(float dB)
    {
        audioMixer.SetFloat(parameterName, dB);
        PlayerPrefs.SetFloat(parameterName, dB);
    }
}
