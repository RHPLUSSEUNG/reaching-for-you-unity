using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Image titleImage;
    [SerializeField] GameObject settingPanel;
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button exitButton;
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        newGameButton.onClick.AddListener(OnClickNewGameButton);
        optionButton.onClick.AddListener(OnClickSettingPanel);
        exitButton.onClick.AddListener(() => Application.Quit());
    }

    private void Start()
    {
        canvas.enabled = true;
        settingPanel.SetActive(false);
        CanvasManager.Instance.ChangeCanvasOrder(canvas);
        StartCoroutine(FadeTitleImage());
    }

    void OnClickSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    void OnClickNewGameButton()
    {
        ComicManager.Instance.ShowComic(ComicType.INTRO);
    }

    IEnumerator FadeTitleImage()
    {
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * 0.75f;
            titleImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
