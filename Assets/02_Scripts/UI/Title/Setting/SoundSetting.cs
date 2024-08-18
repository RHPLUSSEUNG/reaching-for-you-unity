using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    //[TODO:LSH] 계층 구조 확정 후 리팩터 필요
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSoundSlider;    
    [SerializeField] Slider bgmSoundSlider;    
    [SerializeField] Slider sfxSoundSlider;    

    private void Start()
    {
        masterSoundSlider.onValueChanged.AddListener(OnMasterSoundChanged);
        bgmSoundSlider.onValueChanged.AddListener(OnBGMSoundChanged);
        sfxSoundSlider.onValueChanged.AddListener(OnSFXSoundChanged);

        masterSoundSlider.value = 0.5f;
        bgmSoundSlider.value = 0.5f;
        sfxSoundSlider.value = 0.5f;
    }

    void OnMasterSoundChanged(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        SoundManager.Instance.SetBGMVolume(value);
        SoundManager.Instance.SetSFXVolume(value);
    }

    void OnBGMSoundChanged(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        SoundManager.Instance.SetBGMVolume(value);
    }
    
    void OnSFXSoundChanged(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        SoundManager.Instance.SetSFXVolume(value);
    }
}
