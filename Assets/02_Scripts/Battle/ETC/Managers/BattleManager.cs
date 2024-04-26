using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    
    public void BattleReady() {
        //Make Monster Party
        Managers.PlayerButton.UpdateStartButton();
        battleState = BattleState.Start;
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
    }

    public void NextTurn()
    {
        List<GameObject> party;
        if (battleState == BattleState.PlayerTurn)
        {
            party = Managers.Party.playerParty;
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            party = Managers.Party.monsterParty;
            battleState = BattleState.PlayerTurn;

        }
        foreach (GameObject g in party)
        {
            CharacterSpec spec = g.GetComponent<CharacterSpec>();
            foreach(Buff buff in spec.buffs)
            {
                buff.TimeCheck();
            }
            foreach (Debuff debuff in spec.debuffs)
            {
                debuff.TimeCheck();
            }
            spec.remainStamina = spec.stamina;
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
