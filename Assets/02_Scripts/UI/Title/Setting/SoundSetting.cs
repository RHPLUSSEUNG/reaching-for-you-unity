using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    //[TODO:LSH] ���� ���� Ȯ�� �� ������ �ʿ�
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSoundSlider;    
    [SerializeField] Slider bgmSoundSlider;    
    [SerializeField] Slider sfxSoundSlider;    

    private void Start()
    {
        masterSoundSlider.onValueChanged.AddListener(OnMasterSoundChanged);
        bgmSoundSlider.onValueChanged.AddListener(OnBGMSoundChanged);
        sfxSoundSlider.onValueChanged.AddListener(OnSFXSoundChanged);        
    }

    void OnMasterSoundChanged(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    void OnBGMSoundChanged(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }
    
    void OnSFXSoundChanged(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
}
