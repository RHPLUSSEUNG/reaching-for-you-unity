using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager
{
    public List<GameObject> playerParty = new();
    public List<GameObject> monsterParty = new();

    public bool AddParty(string character)
    {
        if (playerParty.Count < 5 && FindPlayer(character)== null)
        {
            GameObject chr = Managers.Party.InstantiatePlayer(character);
            playerParty.Add(chr);
            return true;
        }
        return false;
    }

    public bool RemoveParty(string character)
    {
        if (playerParty.Count > 2) 
        {
            playerParty.Remove(FindPlayer(character));
            return true;
        }
        return false;
    }
    public bool AddParty(GameObject character)
    {
        if (FindPlayer(character.name) != null)
        {
            return false;
        }
        if (playerParty.Count > 5)
        {
            Debug.Log("Too many character in your party");
            return false;
        }
        playerParty.Add(character);
        return true;
    }

    public bool RemoveParty(GameObject character)
    {
        if (playerParty.Count == 1)
        {
            Debug.Log("You don't have any other character in your party");
            return false;
        }
        playerParty.Remove(character);
        return true;
    }

    public GameObject FindPlayer(string name)
    {
        foreach(GameObject character in playerParty)
        {
            if (character.name == $"{name}(Clone)")
            {
                return character;
            }
        }
        return null;
    }

    public void MakeMonsterParty(int numberofMonster)
    {
        MapList map = RandomMap();
        monsterParty.Clear();
        Array monsterList = Managers.Data.GetMonsterList(map);
        System.Random rand = new System.Random();
        string name;
        int idx;
        for (int i = 0; i < numberofMonster; i++)
        {
            idx = rand.Next(0, monsterList.Length);
            name = monsterList.GetValue(idx).ToString();
            InstantiateMonster(name);
        }
    }
    private void AddMonster(GameObject character)
    {
        monsterParty.Add(character);
    }

    private MapList RandomMap()
    {
        Array values = Enum.GetValues(typeof(MapList));
        return (MapList)values.GetValue(new System.Random().Next(0, values.Length));
    }
    public void ClearMonster()
    {
        monsterParty.Clear();
    }

    public GameObject InstantiatePlayer(string character)
    {
        GameObject go = Managers.Prefab.Instantiate($"Character/{character}");
        if (AddParty(go))
        {
            return go;
        }
        return null;
    }
    public GameObject InstantiateMonster(string character, bool afterTurn = false)
    {
        GameObject go = Managers.Prefab.Instantiate($"Monster/{character}");
        AddMonster(go);
        if (afterTurn)
        {
            Managers.Battle.InstantAfterPhaseList.Add(go);
        }
        else
        {
            Managers.Battle.ObjectList.Add(go);
        }
        Managers.Battle.monsterLive++;
        return go;
    }
}
