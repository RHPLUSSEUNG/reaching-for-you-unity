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

    Coroutine currentFadeCoroutine;

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
            if (bgmAudio.isPlaying)
            {
                if (currentFadeCoroutine != null)
                {
                    StopCoroutine(currentFadeCoroutine);
                }

                currentFadeCoroutine = StartCoroutine(CrossFadeBGM(clip, loop, volume));
            }
            else
            {
                bgmAudio.clip = clip;
                bgmAudio.loop = loop;
                bgmAudio.Play();
            }
        }
        else
        {
            Debug.LogWarning("Requested BGM clip not found: " + bgmName);
        }
    }

    IEnumerator CrossFadeBGM(AudioClip newClip, bool loop, float targetVolume)
    {
        float fadeTime = 1f;        

        while (bgmAudio.volume > 0)
        {
            bgmAudio.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }

        bgmAudio.clip = newClip;
        bgmAudio.loop = loop;
        bgmAudio.Play();

        while (bgmAudio.volume < targetVolume)
        {
            bgmAudio.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public void StopMusic()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
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

