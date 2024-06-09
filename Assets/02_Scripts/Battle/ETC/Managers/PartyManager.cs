using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PartyManager
{
    public List<GameObject> playerParty = new();
    public List<GameObject> monsterParty = new();

    public bool AddParty(GameObject character)
    {
        if (FindPlayer(character.name))
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
            if (character.name == name)
            {
                return character;
            }
        }
        return null;
    }

    public void AddMonster(GameObject character)
    {
        monsterParty.Add(character);
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
    public GameObject InstantiateMonster(string character)
    {
        GameObject go = Managers.Prefab.Instantiate($"Monster/{character}");
        AddMonster(go);
        Managers.Battle.ObjectList.Add(go);
        return go;
    }
}
