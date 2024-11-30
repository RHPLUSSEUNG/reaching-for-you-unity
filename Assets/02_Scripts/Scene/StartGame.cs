using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //각종 리소스와 매니저들 로드 필요
    private void Awake()
    {
        LoadSceneManager.LoadScene(SceneType.MAINMENU);
    }
}
