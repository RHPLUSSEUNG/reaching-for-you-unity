using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType
{
    MAINMENU,
    ACADEMY,
    PM_ADVENTURE,
    PM_COMBAT,
    NONE,
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
            case SceneType.MAINMENU:
                {
                    nextScene = "02_MainMenu";
                    break;
                }
            case SceneType.ACADEMY:
                {
                    nextScene = "03_Academy";
                    break;
                }
            case SceneType.PM_ADVENTURE:
                {
                    nextScene = "Adventure_PT_5";
                    break;
                }
            case SceneType.PM_COMBAT:
                {
                    nextScene = "Battle_PT_5";
                    break;
                }
            default:
                {
                    break;
                }
        }

        SceneManager.LoadScene("01_LoadingScene");
    }

    void NextSceneBGMPlay(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.MAINMENU:
                {
                    SoundManager.Instance.PlayMusic("02_Main_Theme");
                    break;
                }
            case SceneType.ACADEMY:
                {
                    SoundManager.Instance.PlayMusic("03_Academy");
                    break;
                }
            case SceneType.PM_ADVENTURE:
                {
                    SoundManager.Instance.PlayMusic("04_Desert_Adventure");
                    break;
                }
            case SceneType.PM_COMBAT:
                {
                    SoundManager.Instance.PlayMusic("06_Desert_InBattle");
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
