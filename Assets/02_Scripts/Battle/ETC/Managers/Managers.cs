using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public static PrefabManager Prefab { get {  return _prefab; } }

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
        _playerButton.Bind();
        _data.OnAwake();
        _battle.BattleReady();
        raycast.TestInit();         // Temp
    }
    #endregion
    public void Update()
    {
        raycast.OnUpdate();           
    }

}
