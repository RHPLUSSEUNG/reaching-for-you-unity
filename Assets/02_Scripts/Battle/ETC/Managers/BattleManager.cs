using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


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
        Managers.Party.AddMonster(GameObject.Find("Enemy"));
        Managers.Party.AddParty(GameObject.Find("Player_Girl"));

        Managers.PlayerButton.UpdateStartButton();
        battleState = BattleState.Start;
    }

    public void BattleStart()
    {
        ObjectList.Clear();
        foreach (GameObject character in Managers.Party.playerParty)
        {
            GameObject go = character.GetComponent<CharacterBattle>().Spawn();
            ObjectList.Add(go);
        }
        foreach (GameObject character in Managers.Party.monsterParty)
        {
            GameObject go = character.GetComponent<CharacterBattle>().Spawn();
            ObjectList.Add(go);
        }
        playerLive = (short)Managers.Party.playerParty.Count;
        monsterLive = (short)Managers.Party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Debug.Log(ObjectList[i].name);
        }
        GameObject.Find("Enemy").SetActive(false);
        GameObject.Find("Player_Girl").SetActive(false);
        NextTurn();
        //TODO Object Turn order sorting
    }

    public void CalcTurn()
    {
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
        ui.ShowActPanel(currentCharacter.GetComponent<PlayerSpec>());
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

        CalcTurn();
        currentCharacter = ObjectList[turnCnt];
        CharacterSpec spec = currentCharacter.GetComponent<CharacterSpec>();
        /*
        if(spec.buffs.Count != 0)
        {
            foreach (Buff buff in spec.buffs)
            {
                buff.TimeCheck();
            }
        }
        if(spec.debuffs.Count != 0)
        {
            foreach (Debuff debuff in spec.debuffs)
            {
                debuff.TimeCheck();
            }
        }
        */
        spec.remainStamina = spec.stamina;
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
        else return;
    }
}
