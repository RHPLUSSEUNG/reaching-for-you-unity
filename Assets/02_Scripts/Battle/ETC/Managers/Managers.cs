using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    #region Setting
    static Managers _manager;
    public static Managers Manager { get { return _manager; } }

    static RaycastManager _raycast = new();
    public static RaycastManager raycast { get { return _raycast; } }
    static PlayerButtonManager _playerButton = new();
    public static PlayerButtonManager PlayerButton { get { return _playerButton; } }
    static BattleManager _battle = new();
    public static BattleManager Battle { get { return _battle; } }
    static PartyManager _party = new();
    public static PartyManager Party { get { return _party; } }
    static ItemManager _item = new();
    public static ItemManager Item { get { return _item; } }
    static DataManager _data = new();
    public static DataManager Data { get { return _data; } }
    static PrefabManager _prefab = new();
    public static PrefabManager Prefab { get { return _prefab; } }
    static ActiveManager _active = new();
    public static ActiveManager Active { get { return _active; } }
    static SkillManager _skill = new();
    public static SkillManager Skill { get { return _skill; } }
    static UIManager _ui = new();
    public static UIManager UI { get { return _ui; } }
    static BattleUIManager _battleui = new();
    public static BattleUIManager BattleUI { get { return _battleui; } }
    static InvenUIManager _invenui = new();
    public static InvenUIManager InvenUI { get { return _invenui; } }

    public void Awake()
    {
        if (_manager == null)
        {
            GameObject gameObject = GameObject.Find("Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "Managers" };
            }
            if (gameObject.GetComponent<Managers>() == null)
            {
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);
            _manager = gameObject.GetComponent<Managers>();
        }
    }
    #endregion
    public void Update()
    {
        //[2024-09-30][LSH's Code]: [enter-adventure-map-ui]
        if (LoadSceneManager.sceneType != SceneType.ACADEMY)
        {
            raycast.OnUpdate();
        }          
    }

    public void OnEnable()
    {
        _data.OnAwake();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        Debug.Log(mode);
        
        switch (scene.name)
        {
            case "05_Battle":
                _skill.OnAwake();
                _battle.BattleReady();
                _raycast.OnStart();
                break;
            case "TITLE_PT_5":
                _data.OnAwake();
                break;
        }
    }
}
