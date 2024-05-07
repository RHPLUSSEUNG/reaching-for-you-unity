using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    public List<GameObject> ObjectList = new List<GameObject>();
    int turnCnt = 0;
    public void BattleReady() {
        //TOOD make monster party

        Managers.PlayerButton.UpdateStartButton();
        battleState = BattleState.Start;
        Managers.Party.AddParty(GameObject.Find("Player"));
        Managers.Party.monsterParty.Add(GameObject.Find("Monster"));
    }

    public void BattleStart()
    {
        foreach (GameObject character in Managers.Party.playerParty)
        {
            character.GetComponent<CharacterBattle>().Spawn();
        }
        foreach (GameObject character in Managers.Party.monsterParty)
        {
            character.GetComponent<CharacterBattle>().Spawn();
            //Generate Character on Map
        }
        playerLive = (short)Managers.Party.playerParty.Count;
        monsterLive = (short)Managers.Party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;

        //TODO Object List Reset
        ObjectList.Clear();
    }

    public void NextTurn()
    {
        CharacterSpec spec = ObjectList[turnCnt%ObjectList.Count].GetComponent<CharacterSpec>();
        foreach (Buff buff in spec.buffs)
        {
            buff.TimeCheck();
        }
        foreach (Debuff debuff in spec.debuffs)
        {
            debuff.TimeCheck();
        }
        spec.remainStamina = spec.stamina;
        turnCnt++;
        if (ObjectList[turnCnt % ObjectList.Count].CompareTag("Player"))
        {
            battleState = BattleState.PlayerTurn;
            Managers.PlayerButton.UpdateSkillButton(ObjectList[turnCnt % ObjectList.Count]);
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            //Monster AI
            NextTurn();
        }
    }

    public void Result()
    {
        if (playerLive == 0)
        {
            battleState = BattleState.Victory;
        }
        else if (monsterLive == 0)
        {
            battleState = BattleState.Defeat;
        }
        else return;
    }
}
