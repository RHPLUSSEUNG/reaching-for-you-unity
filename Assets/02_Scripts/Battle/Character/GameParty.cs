using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParty : MonoBehaviour
{
    #region singleton
    static GameParty _party;
    public static GameParty party {  get { return _party; } }

    public void Init()
    {
        GameObject go = GameObject.Find("Manager");
        if (go == null)
        {
            go = new GameObject { name = "Manager" };
            go.GetComponent<GameParty>();
        }
        if (go.GetComponent<GameParty>() == null)
        {
            go.AddComponent<GameParty>();
        }
        DontDestroyOnLoad(go);
        _party = go.GetComponent<GameParty>();
    }
    #endregion

    public List<GameObject> playerParty = new List<GameObject>();
    public List<GameObject> monsterParty = new List<GameObject>();

    public void AddParty(GameObject character)
    {
        if (playerParty.Count > 4)
        {
            Debug.Log("Too many character in your party");
            return;
        }
        playerParty.Add(character);
    }

    public void RemoveParty(GameObject character)
    {
        if (playerParty.Count == 1)
        {
            Debug.Log("You don't have any other character in your party");
            return;
        }
        playerParty.Remove(character);
    }
}
