using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //���� ���ҽ��� �Ŵ����� �ε� �ʿ�
    private void Awake()
    {
        LoadSceneManager.LoadScene(SceneType.MAINMENU);
    }
}
