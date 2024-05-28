using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    public List<GameObject> ObjectList = new();
    public GameObject currentCharacter;
    public UI_ActPanel ui;
    int turnCnt = 0;
    public List<GameObject> Areas = new();

    int compareDefense(GameObject character1,  GameObject character2)
    {
        return character1.GetComponent<EntityStat>().Defense < character2.GetComponent<EntityStat>().Defense ? -1 : 1;
    }



    public void BattleReady()
    {
        ui = GameObject.Find("BattleUI").transform.GetChild(7).GetComponent<UI_ActPanel>();
        ui.gameObject.SetActive(false);
        //TOOD make monster party
        Managers.Party.AddMonster(Managers.Prefab.Instantiate($"Monster/Enemy_Crab"));
        Managers.Party.AddParty(Managers.Prefab.Instantiate($"Character/Player_Girl_Battle"));
        ObjectList.Clear();
        foreach (GameObject character in Managers.Party.playerParty)
        {
            character.GetComponent<SkillList>().AddSkill(Managers.Skill.Instantiate(3));
            ObjectList.Add(character);
        }
        foreach (GameObject character in Managers.Party.monsterParty)
        {
            ObjectList.Add(character);
        }
        ObjectList.Sort(compareDefense);
        Managers.PlayerButton.UpdateStartButton();
        battleState = BattleState.Start;
        turnCnt = -1;
    }

    public void BattleStart()
    {
        playerLive = (short)Managers.Party.playerParty.Count;
        monsterLive = (short)Managers.Party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;
        NextTurn();
        //TODO Object Turn order sorting
    }

    public void CalcTurn()
    {
        if(currentCharacter != null && currentCharacter.GetComponent<CharacterState>() != null)
        {
            currentCharacter.GetComponent<CharacterState>().CalcTurn();
        }

        turnCnt++;
        turnCnt %= ObjectList.Count;
    }

    public void PlayerTurn()
    {
        battleState = BattleState.PlayerTurn;
        Managers.PlayerButton.UpdateSkillButton(currentCharacter);
        ui.ShowActPanel(currentCharacter.GetComponent<SkillList>());
        Debug.Log("PlayerTurn Start");

    }

    public void EnemyTurn(GameObject character)
    {
        Debug.Log(character.name);
        if (character.GetComponent<EnemyAI_Test>() == null)
        {
            Debug.Log("Component Error");
            return;
        }
        Debug.Log("EnemyTurn Start");
        battleState = BattleState.EnemyTurn;
        character.GetComponent<EnemyAI_Test>().ProceedTurn();
    }

    public void NextTurn()
    {
        if (battleState == BattleState.Defeat || battleState == BattleState.Start || battleState == BattleState.Victory)
        {
            return;
        }
        Managers.raycast.detect_ready = false;
        CalcTurn();
        currentCharacter = ObjectList[turnCnt];
        foreach(GameObject area  in Areas) 
        {
            area.GetComponent<AreaInterface>().CalcTurn();
            Debug.Log(area.name);
        }

        if(currentCharacter.GetComponent<CharacterState>().IsStun())
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
    }
}
