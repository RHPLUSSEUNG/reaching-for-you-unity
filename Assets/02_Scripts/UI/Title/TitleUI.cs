using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button exitButton;

    private void Awake()
    {        
        newGameButton.onClick.AddListener(()=>SceneChanger.Instance.ChangeScene(SceneType.AM));
        exitButton.onClick.AddListener(()=>Application.Quit());
    }    
}
