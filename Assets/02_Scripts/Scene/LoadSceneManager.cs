using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType
{
    TITLE,
    AM,
    PM,
    BATTLE,

}

public class LoadSceneManager : MonoBehaviour
{
    public static string nextScene;
    public static SceneType sceneType;    
    [SerializeField] Image logoImage;
    [SerializeField] Image progressBar;
    
    public float speed = 1.0f;
    public float maxAlpha = 1.0f;
    public float minAlpha = 0.0f;

    private void Start()
    {        
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(SceneType _sceneType)
    {
        sceneType = _sceneType;
        
        switch (sceneType)
        {
            case SceneType.TITLE:
                {
                    nextScene = "ui-settings";
                    break;
                }
            case SceneType.AM:
                {
                    nextScene = "AM_PT_3";
                    break;
                }
            case SceneType.PM:
                {
                    break;
                }
            case SceneType.BATTLE:
                {
                    nextScene = "BATTLE_PT_3";
                    break;
                }
            default:
                {
                    break;
                }
        }

        SceneManager.LoadScene("LOADING_PT_3");
    }

    void NextSceneBGMPlay(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.TITLE:
                {
                    SoundManager.Instance.PlayMusic("Main_Theme");
                    break;
                }
            case SceneType.AM:
                {
                    SoundManager.Instance.PlayMusic("AM_School");
                    break;
                }
            case SceneType.PM:
                {
                    //SoundManager.Instance.PlayMusic("Main_Theme");
                    break;
                }
            case SceneType.BATTLE:
                {
                    SoundManager.Instance.PlayMusic("Stage1_Battle");
                    break;
                }
        }
    }

    IEnumerator LoadScene()
    {        
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.LoadAudioClips(sceneType);

        while (!op.isDone)
        {
            yield return null;
            StartCoroutine(BlinkLogo());
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    
                    yield return new WaitForSeconds(3.0f);
                    StopCoroutine(BlinkLogo());
                    NextSceneBGMPlay(sceneType);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    IEnumerator BlinkLogo()
    {        
        while (true)
        {
            yield return null;

            float alpha = Mathf.PingPong(Time.time * speed, maxAlpha - minAlpha) + minAlpha;            

            Color color = logoImage.color;           
            color.a = alpha;
            logoImage.color = color;
        }
    }
}
