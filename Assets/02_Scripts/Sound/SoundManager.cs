using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadAudioClips(SceneType sceneType)
    {
        bgmClips.Clear();

        switch (sceneType)
        {
            case SceneType.TITLE:
                {
                    foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Sounds/BGM/Title"))
                    {
                        bgmClips[bgm.name] = bgm;
                    }
                    break;
                }
            case SceneType.AM:
                {
                    foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Sounds/BGM/AM"))
                    {
                        bgmClips[bgm.name] = bgm;
                    }
                    break;
                }
            case SceneType.PM:
                {
                    foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Sounds/BGM/Adventure"))
                    {
                        bgmClips[bgm.name] = bgm;
                    }
                    break;
                }
            case SceneType.BATTLE:
                {
                    foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Sounds/BGM/Battle"))
                    {
                        bgmClips[bgm.name] = bgm;
                    }
                    break;
                }
        }
    }

    public void PlayMusic(string bgmName, bool loop = true, float targetVolume = 1.0f)
    {
        if (bgmClips.TryGetValue(bgmName, out AudioClip clip))
        {
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            currentFadeCoroutine = StartCoroutine(FadeIn(bgmAudio, clip, loop, targetVolume, 1.5f));
        }
        else
        {
            Debug.LogWarning("Requested BGM clip not found: " + bgmName);
        }
    }

    public void StopMusic()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        
        currentFadeCoroutine = StartCoroutine(FadeOut(bgmAudio, 1.5f));
    }


    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0;
    }

    IEnumerator FadeIn(AudioSource audioSource, AudioClip clip, bool loop, float targetVolume, float fadeTime)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        audioSource.volume = 0;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime * targetVolume;
            yield return null;
        }
    }

    //씬 로드 없이 BGM이 바뀔 때 필요
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

    public void SetBGMVolume(float volume)
    {
        bgmAudio.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudio.volume = volume;
    }
}

