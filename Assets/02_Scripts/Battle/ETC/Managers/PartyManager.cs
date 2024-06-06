using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PartyManager
{
    public List<GameObject> playerParty = new();
    public List<GameObject> monsterParty = new();

    public void AddParty(GameObject character)
    {
        if (playerParty.Count > 5)
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

    public void AddMonster(GameObject character)
    {
        monsterParty.Add(character);
    }

    public void ClearMonster()
    {
        monsterParty.Clear();
    }

    public void InstantiatePlayer(GameObject character)
    {
        GameObject go = Managers.Prefab.Instantiate($"Character/{character.name}");
        Managers.Battle.ObjectList.Add(go);
    }
    public void InstantiateMonster(GameObject character)
    {
        GameObject go = Managers.Prefab.Instantiate($"Monster/{character.name}");
        Managers.Battle.ObjectList.Add(go);
    }
}
