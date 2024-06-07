using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        LoadSceneManager.LoadScene("TITLE_PT_3");
    }
}
