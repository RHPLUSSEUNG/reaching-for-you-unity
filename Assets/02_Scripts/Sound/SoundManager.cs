using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager:MonoBehaviour
{
    public static SoundManager Instance;   

    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource sfxAudio;
    Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Instance = this;
        LoadAudioClips();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayMusic("Main_Theme");
    }

    public void LoadAudioClips()
    {
        foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Sounds/BGM"))
        {
            bgmClips[bgm.name] = bgm;
        }

        foreach (AudioClip sfx in Resources.LoadAll<AudioClip>("Sounds/SFX"))
        {
            sfxClips[sfx.name] = sfx;
        }
    }

    public void PlayMusic(string bgmName, bool loop = true, float volume = 1.0f)
    {
        if (bgmClips.TryGetValue(bgmName, out AudioClip clip))
        {
            bgmAudio.clip = clip;
            bgmAudio.loop = loop;
            bgmAudio.volume = volume;
            bgmAudio.Play();
        }
        else
        {
            Debug.LogWarning("Requested BGM clip not found: " + bgmName);
        }
    }

    public void StopMusic()
    {
        bgmAudio.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        bgmAudio.volume = volume;
    }

    public void SetSfxsVolume(float volume)
    {
        sfxAudio.volume = volume;
    }
}

