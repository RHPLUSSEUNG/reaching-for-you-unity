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

    private void Awake()
    {        
        newGameButton.onClick.AddListener(()=>SceneChanger.Instance.ChangeScene(SceneType.AM));
        optionButton.onClick.AddListener(OnClickSettingPanel);
        exitButton.onClick.AddListener(()=>Application.Quit());
    }

    private void Start()
    {
        settingPanel.SetActive(false);
    }

    void OnClickSettingPanel()
    {
        settingPanel.SetActive(true);
    }

}
