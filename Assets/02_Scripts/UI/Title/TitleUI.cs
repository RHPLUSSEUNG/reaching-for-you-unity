using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
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
        exitButton.onClick.AddListener(()=>Application.Quit());
    }

    private void Start()
    {
        canvas.enabled = true;
        settingPanel.SetActive(false);
    }

    void OnClickSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    void OnClickNewGameButton()
    {        
        ComicManager.Instance.ShowComic(ComicType.INTRO);
    }    
}
