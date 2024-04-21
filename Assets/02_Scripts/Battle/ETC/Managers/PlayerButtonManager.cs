using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Vector3 skillPos = Vector3.zero;
    Active skill = null;

    public void Bind()
    {
        GameObject go = GameObject.Find("BattleUI");
        button1 = go.transform.GetChild(0).GetComponent<Button>();
        button2= go.transform.GetChild(1).GetComponent<Button>();
        button3 = go.transform.GetChild(2).GetComponent<Button>();
    }

    public void UpdateButton(GameObject player)
    {
        Debug.Log("clear");
        this.player = player;
        this.spec = this.player.GetComponent<PlayerSpec>();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(Skill1);
        button2.onClick.AddListener(Skill2);
        button3.onClick.AddListener(Skill3);
    }

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

    public void Cancle()
    {
        state = ButtonState.Idle;
    }

    public void SetSkillPos()
    {
        skillPos = Input.mousePosition;
        skill.Activate(skillPos);
        state = ButtonState.Idle;
    }
}
