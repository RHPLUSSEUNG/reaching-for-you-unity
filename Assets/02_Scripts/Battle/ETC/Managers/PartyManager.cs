using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

    public void AddMonster(GameObject character)
    {
        monsterParty.Add(character);
    }

    public void ClearMonster()
    {
        monsterParty.Clear();
    }

    public void Damage(GameObject target)
    {
        GameObject character = Managers.Battle.currentCharacter;
        target.GetComponent<EntityStat>().Hp -= character.GetComponent<EntityStat>().BaseDamage;
        if(target.GetComponent<EntityStat>().Hp <= 0)
        {
            Dead(target);
        }
    }

    public void Damage(GameObject target, int damage)
    {
        target.GetComponent<EntityStat>().Hp -= damage;
        if (target.GetComponent<EntityStat>().Hp <= 0)
        {
            Dead(target);
        }
    }

    public void Heal(GameObject target, int heal)
    {
        target.GetComponent<EntityStat>().Hp += heal;
    }

    public void Dead(GameObject character)
    {
        if (character.CompareTag("Player"))
        {
            Managers.Battle.playerLive--;
        }
        else
        {
            Managers.Battle.monsterLive--;
        }
        Managers.Battle.ObjectList.Remove(character);
        character.SetActive(false);
        Managers.Battle.Result();
    }
}
