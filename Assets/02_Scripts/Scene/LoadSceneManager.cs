using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TMP_Text continueText;
    [SerializeField] Image logoImage;
    [SerializeField] Image progressImage;

    public float speed = 1.0f;
    public float maxAlpha = 1.0f;
    public float minAlpha = 0.0f;

    private void Start()
    {
        continueText.text = "";
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
                    int stageNumber = AdventureManager.StageNumber;
                    switch (stageNumber)
                    {
                        case 0:
                            SoundManager.Instance.PlayMusic("04_Desert_Adventure");
                            break;
                        case 1:
                            SoundManager.Instance.PlayMusic("08_Ocean_Adventure");
                            break;
                    }
                    break;
                }
            case SceneType.PM_COMBAT:
                {
                    int stageNumber = AdventureManager.StageNumber;
                    switch (stageNumber)
                    {
                        case 0:
                            SoundManager.Instance.PlayMusic("06_Desert_InBattle");
                            break;
                        case 1:
                            SoundManager.Instance.PlayMusic("08_Ocean_InBattle");
                            break;
                    }
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
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressImage.fillAmount = Mathf.Lerp(progressImage.fillAmount, op.progress, timer);
                if (progressImage.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressImage.fillAmount = Mathf.Lerp(progressImage.fillAmount, 1f, timer);
                if (progressImage.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(1.0f);
                    NextSceneBGMPlay(sceneType);
                    continueText.text = "-클릭하여 계속-";
                    StartCoroutine(BlinkText());
                    yield break;
                }
            }
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            yield return null;
            float alpha = Mathf.PingPong(Time.time * speed, maxAlpha - minAlpha) + minAlpha;
            Color color = continueText.color;
            color.a = alpha;
            continueText.color = color;
        }
    }

    private void Update()
    {
        if (continueText.gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
