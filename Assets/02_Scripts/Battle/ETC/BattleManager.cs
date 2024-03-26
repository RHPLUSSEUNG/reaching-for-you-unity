using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region singleton
    static BattleManager _instance;
    public static BattleManager Instance {  get { return _instance; } }
    public static Inventory inven = new Inventory();
    #endregion
    GameObject player;
    List<GameObject> playerParty = new List<GameObject>();

    public void Init()
    {
        GameObject go = GameObject.Find("BattleManager");
        if (go == null)
        {
            go = new GameObject { name = "BattleManager" };
            go.GetComponent<BattleManager>();
        }
        DontDestroyOnLoad(go);
        _instance = go.GetComponent<BattleManager>();
    }

    public void Start()
    {
        //client's player find
        player = FindObjectOfType<PlayerSpec>().gameObject;
        AddParty(player);
    }

    //player party increase
    public void AddParty(GameObject character)
    {
        if(playerParty.Count > 4)
        {
            Debug.Log("Too many character in your party");
            return;
        }
        playerParty.Add(character);
    }
    //player party decrease
    public void RemoveParty(GameObject character)
    {
        if(playerParty.Count < 2)
        {
            Debug.Log("You don't have any other character in your party");
            return;
        }
        playerParty.Remove(character);
    }

    public void BattleStart()
    {
        foreach(GameObject character in playerParty)
        {
            //Generate Character on Map
            Instantiate(character, new Vector3() , new Quaternion());
        }
    }
}
