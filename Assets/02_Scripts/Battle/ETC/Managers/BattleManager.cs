using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. Character TurnSpeed에 따라서 턴 순서가 정해진다 (세나 결투장)
// 2. Player Turn -> Enemy Turn (모든 캐릭터 합쳐서 다 실행)

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
    }

    public void BattleStart()
    {
        ObjectList.Clear();
        foreach (GameObject character in Managers.Party.playerParty)
        {
            character.GetComponent<CharacterBattle>().Spawn();
            ObjectList.Add(character);
        }
        foreach (GameObject character in Managers.Party.monsterParty)
        {
            character.GetComponent<CharacterBattle>().Spawn();
            ObjectList.Add(character);
        }
        playerLive = (short)Managers.Party.playerParty.Count;
        monsterLive = (short)Managers.Party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;

        //TODO Object Turn order sorting
    }

    public void NextTurn()
    {
        if (playerLive == 0 || monsterLive == 0)
        {
            Result();
        }
        else
        {
            GameObject Character = ObjectList[turnCnt % ObjectList.Count];
            CharacterSpec spec = ObjectList[turnCnt % ObjectList.Count].GetComponent<CharacterSpec>();
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
                Character.GetComponent<EnemyAI_Test>().ProceedTurn();
                NextTurn();
            }
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
