using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonManager
{
    public ButtonState state = ButtonState.Idle;
    public GameObject player;
    public PlayerSpec spec;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button nextTurn;
    public Button cancle;

    Active skill = null;

    public void Bind()
    {
        GameObject go = GameObject.Find("BattleUI");
        button1 = go.transform.GetChild(0).GetComponent<Button>();
        button2= go.transform.GetChild(1).GetComponent<Button>();
        button3 = go.transform.GetChild(2).GetComponent<Button>();
        button4 = go.transform.GetChild(3).GetComponent<Button>();
        button5 = go.transform.GetChild(4).GetComponent<Button>();
        nextTurn = go.transform.GetChild(5).GetComponent<Button>();
        cancle = go.transform.GetChild(6).GetComponent<Button>();
    }

    public void UpdateStartButton()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button5.onClick.RemoveAllListeners();
        nextTurn.onClick.RemoveAllListeners();
        cancle.onClick.RemoveAllListeners();


        button1.onClick.AddListener(Player1);
        button2.onClick.AddListener(Player2);
        button3.onClick.AddListener(Player3);
        button4.onClick.AddListener(Player4);
        button5.onClick.AddListener(Player5);
        nextTurn.onClick.AddListener(NextTurn);
        cancle.onClick.AddListener(Cancle);
    }

    public void UpdateSkillButton(GameObject player)
    {
        Debug.Log("Update Skill Button");
        this.player = player;
        this.spec = this.player.GetComponent<PlayerSpec>();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button5.onClick.RemoveAllListeners();

        button1.onClick.AddListener(Skill1);
        button2.onClick.AddListener(Skill2);
        button3.onClick.AddListener(Skill3);
        button4.onClick.AddListener(Skill4);
        button5.onClick.AddListener(Skill5);
    }
    #region Skill
    public void Skill1()
    {
        skill = spec._equipSkills[0] as Active;
        
        state = ButtonState.Skill;
    }

    public void Skill2()
    {
        skill = spec._equipSkills[1] as Active;
        
        state = ButtonState.Skill;
    }

    public void Skill3()
    {
        skill = spec._equipSkills[2] as Active;
        
        state = ButtonState.Skill;
    }

    public void Skill4()
    {
        skill = spec._equipSkills[3] as Active;

        state = ButtonState.Skill;
    }

    public void Skill5()
    {
        skill = spec._equipSkills[4] as Active;

        state = ButtonState.Skill;
    }

    public Active GetSkill()
    {
        if (skill == null) return null;
        return skill;
    }
    #endregion

    #region Player
    public void Player1()
    {
        player = Managers.Party.playerParty[0];

        state = ButtonState.PlayerSet;
    }

    public void Player2()
    {
        player = Managers.Party.monsterParty[0];

        state = ButtonState.PlayerSet;
    }

    public void Player3()
    {
        player = Managers.Party.playerParty[2];

        state = ButtonState.PlayerSet;
    }

    public void Player4()
    {
        player = Managers.Party.playerParty[3];

        state = ButtonState.PlayerSet;
    }

    public void Player5()
    {
        player = Managers.Party.playerParty[4];

        state = ButtonState.PlayerSet;
    }

    public void SetPosition(GameObject pos)
    {
        player.GetComponent<CharacterSpec>().pos = pos;
        state = ButtonState.Idle;
    }
    #endregion

    public void Cancle()
    {
        state = ButtonState.Idle;
    }

    public void NextTurn()
    {
        if(Managers.Battle.battleState == BattleState.PlayerTurn)
        {
            Managers.Battle.NextTurn();
            Debug.Log("PlayerTurn end");
        }
        else if(Managers.Battle.battleState == BattleState.Start)
        {
            Debug.Log("Battle Start");
            Managers.Battle.BattleStart();
        }
    }

}
