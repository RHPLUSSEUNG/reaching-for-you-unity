using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_UI_Manager : MonoBehaviour
{
    static PM_UI_Manager s_Instance;
    static PM_UI_Manager Instance { get { Init(); return s_Instance; } }

    ResourcesManager resource = new ResourcesManager();
    UIManager ui = new UIManager();
    BattleUIManager battleUI = new BattleUIManager();
    InvenUIManager invenUI = new InvenUIManager();
    public static ResourcesManager Resource { get { return Instance.resource; } }
    public static UIManager UI { get { return Instance.ui; } }
    public static BattleUIManager BattleUI { get { return Instance.battleUI; } }
    public static InvenUIManager InvenUI { get { return Instance.invenUI; } }

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
            GameObject go = GameObject.Find("@PM_UI_Managers");
            if (go == null)
            {
                go = new GameObject { name = "@PM_UI_Managers" };
                go.AddComponent<PM_UI_Manager>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<PM_UI_Manager>();
        }
    }
}
