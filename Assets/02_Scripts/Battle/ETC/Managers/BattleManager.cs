using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    public List<GameObject> ObjectList = new();
    public GameObject currentCharacter;

    public int totalTurnCnt = 0;
    int turnCnt = 0;
    public List<GameObject> Areas = new();

    int compareDefense(GameObject character1,  GameObject character2)
    {
        return character1.GetComponent<EntityStat>().Defense < character2.GetComponent<EntityStat>().Defense ? -1 : 1;
    }

    public bool Can_Continue()
    {
        if (currentCharacter == null || currentCharacter.CompareTag("Monster"))
        {
            return true;
        }
        else if(battleState == BattleState.Defeat && battleState == BattleState.Victory)
        {
            Debug.Log("Battle is End");
            return false;
        }
        else if (Managers.Skill.is_effect)
        {
            Debug.Log("Wait for skill effect End");
            return false;
        }
        else if (currentCharacter.GetComponent<PlayerBattle>().isMoving)
        {
            Debug.Log("Wait for Character Moving");
            return false;
        }
        return true;
    }

    public void BattleReady()
    {
        //TOOD make monster party
        ObjectList.Clear();
        Managers.Party.InstantiatePlayer("Player_Girl_Battle");
        Managers.Party.InstantiateMonster("Enemy_Crab");
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<SkillList>().AddSkill(0);
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<SkillList>().AddSkill(7);
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<SkillList>().AddSkill(3);
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<SkillList>().AddSkill(8);
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<SkillList>().AddSkill(9);
        battleState = BattleState.Start;
        turnCnt = -1;
    }

    public void BattleStart()
    {
        playerLive = (short)Managers.Party.playerParty.Count;
        monsterLive = (short)Managers.Party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;
        Managers.Skill.ReadyGameSkill();
        ObjectList.Sort(compareDefense);
        NextTurn();
    }

    public void CalcTurn()
    {
        if(currentCharacter != null && currentCharacter.GetComponent<CharacterState>() != null)
        {
            currentCharacter.GetComponent<CharacterState>().CalcTurn();
        }

        turnCnt++;
        totalTurnCnt++;
        turnCnt %= ObjectList.Count;
    }

    public void PlayerTurn()
    {
        battleState = BattleState.PlayerTurn;
        Managers.BattleUI.actUI.UpdateCharacterInfo();
        Debug.Log("PlayerTurn Start");

    }
    public void EnemyTurn(GameObject character)
    {
        Debug.Log(character.name);
        if (character.GetComponent<EnemyAI_Base>() == null)
        {
            Debug.Log("Component Error");
            return;
        }
        Debug.Log("EnemyTurn Start");
        battleState = BattleState.EnemyTurn;
        character.GetComponent<EnemyAI_Base>().ProceedTurn();
        Debug.Log("EnemyTurn End");
    }

    public void NextTurn()
    {
        Managers.Manager.StartCoroutine(NextTurnCoroutine());
    }

    public void Result()
    {
        if (monsterLive == 0)
        {
            battleState = BattleState.Victory;
            Debug.Log("Victory");
        }
        else if (playerLive == 0)
        {
            battleState = BattleState.Defeat;
            Debug.Log("Defeat");
        }

        SceneManager.LoadScene("AM_PT_3");
    }
    public IEnumerator NextTurnCoroutine()
    {
        while(!Can_Continue())
        {
            yield return null;
        }
        CalcTurn();
        currentCharacter = ObjectList[turnCnt];

        if (Areas.Count != 0)
        {
            foreach (GameObject area in Areas)
            {
                area.GetComponent<AreaInterface>().CalcTurn();
                Debug.Log(area.name);
            }
        }
        if (currentCharacter.GetComponent<CharacterState>().IsStun())
        {
            NextTurn();
        }
        if (ObjectList[turnCnt].CompareTag("Player"))
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn(currentCharacter);
        }
        yield break;
    }
}
