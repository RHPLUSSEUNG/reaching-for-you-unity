using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager
{
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
