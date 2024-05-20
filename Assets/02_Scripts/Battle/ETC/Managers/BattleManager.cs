using System.Collections.Generic;
using UnityEngine;


// 1. Character TurnSpeed�� ���� �� ������ �������� (���� ������)
// 2. Player Turn -> Enemy Turn (��� ĳ���� ���ļ� �� ����)

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    public List<GameObject> ObjectList = new List<GameObject>();
    public GameObject currentCharacter;
    public UI_ActPanel ui;
    int turnCnt = 0;
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
            character.GetComponent<SkillList>().AddSkill(Managers.Prefab.Instantiate($"Skill/TestAttack"));
            ObjectList.Add(character);
        }
        foreach (GameObject character in Managers.Party.monsterParty)
        {
            ObjectList.Add(character);
        }

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
        if(currentCharacter != null && currentCharacter.GetComponent<SkillList>() != null)
        {
            currentCharacter.GetComponent<SkillList>().CalcTurn();
        }

        turnCnt++;
        turnCnt %= ObjectList.Count;
        if (playerLive == 0 || monsterLive == 0)
        {
            Result();
        }
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
        
        CalcTurn();
        currentCharacter = ObjectList[turnCnt];
        
        Debug.Log(turnCnt);
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
        //SceneManager.LoadScene("model_test");
    }
}
