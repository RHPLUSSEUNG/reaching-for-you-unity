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

//[LSH:CODE] [Violates SOLID principles, Requiring refactoring]
public class LoadSceneManager : MonoBehaviour
{
    public static string nextScene;
    public static SceneType sceneType;
    [SerializeField] TMP_Text continueText;
    [SerializeField] TMP_Text[] sequenceText;
    [SerializeField] Image logoImage;
    [SerializeField] Image progressImage;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject preMainMenuPanel;
    [SerializeField] Image titleLogoImage;

    public float speed = 1.0f;
    public float maxAlpha = 1.0f;
    public float minAlpha = 0.0f;
    bool isBlinking = true;

    private void Start()
    {
        StartCoroutine(LoadScene());
        if (sceneType == SceneType.MAINMENU)
        {
            preMainMenuPanel.SetActive(true);
            loadingPanel.SetActive(false);
            StartCoroutine(BlinkUIElement(titleLogoImage, 3));
        }
        else
        {
            loadingPanel.SetActive(true);
            preMainMenuPanel.SetActive(false);
            ChangeColorBySceneType(sceneType);
            continueText.text = "";
        }
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
                    nextScene = "04_Adventure";
                    break;
                }
            case SceneType.PM_COMBAT:
                {
                    nextScene = "05_Battle";
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
                    SoundManager.Instance.PlayMusic("BGM_MainMenu_01");
                    break;
                }
            case SceneType.ACADEMY:
                {
                    SoundManager.Instance.PlayMusic("BGM_Academy_01");
                    break;
                }
            case SceneType.PM_ADVENTURE:
                {
                    int stageNumber = AdventureManager.StageNumber;
                    switch (stageNumber)
                    {
                        case 0:
                            SoundManager.Instance.PlayMusic("BGM_Adventure_Desert_01");
                            break;
                        case 1:
                            SoundManager.Instance.PlayMusic("BGM_Adventure_Sea_01");
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
                            SoundManager.Instance.PlayMusic("BGM_Battle_Desert_01");
                            break;
                        case 1:
                            SoundManager.Instance.PlayMusic("BGM_Battle_Sea_01");
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
                if (sceneType != SceneType.MAINMENU)
                {
                    progressImage.fillAmount = Mathf.Lerp(progressImage.fillAmount, 1f, timer);
                }
                if (progressImage.fillAmount == 1.0f || sceneType == SceneType.MAINMENU)
                {                                        
                    if (sceneType == SceneType.MAINMENU)
                    {                        
                        if (titleLogoImage.color.a >= 0.95f && !isBlinking)
                        {                                                                                    
                            op.allowSceneActivation = true;
                            NextSceneBGMPlay(sceneType);
                            StopAllCoroutines();
                            yield break;
                        }                        
                    }
                    else
                    {
                        continueText.text = "-클릭하여 계속-";
                        StartCoroutine(BlinkUIElement(continueText));
                        if (continueText.gameObject.activeSelf && Input.GetMouseButtonDown(0))
                        {                            
                            op.allowSceneActivation = true;
                            NextSceneBGMPlay(sceneType);
                            StopAllCoroutines();
                            yield break;
                        }                        
                    }
                }
            }
        }
    }

    IEnumerator BlinkUIElement(Graphic uiElement, int maxBlinkCount = 0)
    {
        int blinkCount = 0;
        bool isIncreasing = true;

        while (true)
        {
            yield return null;
            float alpha = Mathf.PingPong(Time.time * speed, maxAlpha - minAlpha) + minAlpha;
            Color color = uiElement.color;
            color.a = alpha;
            uiElement.color = color;
            if (maxBlinkCount > 0 && alpha >= 0.95f && isIncreasing)
            {
                blinkCount++;
                isIncreasing = false;
            }
            else if (alpha <= 0.05f && !isIncreasing)
            {
                isIncreasing = true;
            }

            if (blinkCount >= maxBlinkCount)
            {
                isBlinking = false;
            }
        }
    }

    public void ChangeColorBySceneType(SceneType _sceneType)
    {
        sceneType = _sceneType;
        switch (sceneType)
        {
            case SceneType.MAINMENU:
                {
                    Camera.main.backgroundColor = HexToColor("#FFFFFF");                    
                    continueText.color = HexToColor("#000000");
                    for (int i = 0; i < sequenceText.Length; i++)
                    {
                        sequenceText[i].color = HexToColor("#000000");
                    }
                    break;
                }
            case SceneType.ACADEMY:
                {
                    Camera.main.backgroundColor = HexToColor("#FFFFFF");                    
                    continueText.color = HexToColor("#000000");
                    for (int i = 0; i < sequenceText.Length; i++)
                    {
                        sequenceText[i].color = HexToColor("#000000");
                    }
                    break;
                }
            case SceneType.PM_ADVENTURE:
                {
                    Camera.main.backgroundColor = HexToColor("#1D1C21");
                    continueText.color = HexToColor("#FFFFFF");
                    for (int i = 0; i < sequenceText.Length; i++)
                    {
                        sequenceText[i].color = HexToColor("#FFFFFF");
                    }
                    break;
                }
            case SceneType.PM_COMBAT:
                {
                    Camera.main.backgroundColor = HexToColor("#1D1C21");                    
                    continueText.color = HexToColor("#FFFFFF");
                    for (int i = 0; i < sequenceText.Length; i++)
                    {
                        sequenceText[i].color = HexToColor("#FFFFFF");
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.white;
    }

    private void Update()
    {
        
    }
}
