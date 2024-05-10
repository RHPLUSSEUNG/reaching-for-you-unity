using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_UI_Manager : MonoBehaviour
{
    static PM_UI_Manager s_Instance;
    static PM_UI_Manager Instance { get { Init(); return s_Instance; } }

    ResourcesManager resource = new ResourcesManager();
    UIManager ui = new UIManager();

    public static ResourcesManager Resource { get { return Instance.resource; } }
    public static UIManager UI { get { return Instance.ui; } }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<PM_UI_Manager>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<PM_UI_Manager>();
        }
    }
}
