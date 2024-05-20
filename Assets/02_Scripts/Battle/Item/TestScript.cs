using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject skills = GameObject.Find("UsingSkill");

        Debug.Log("Start");
        IncreaseSpeed buff = new();
        Debug.Log("Create Buff");
        buff.SetBuff(5, this.gameObject, 5);
        Debug.Log("Set Buff & Start Buff");

        for(int i= 0; i< 4; i++)
        {
            this.gameObject.GetComponent<SkillList>().CalcTurn();
            Debug.Log($"remain turn : {buff.remainTurn}");
        }

        Debug.Log($"Buff Count: {this.gameObject.GetComponent<SkillList>().buffs.Count}");

        GameObject go = GameObject.Find("TestAttack");
        Debug.Log(go.name);
        for(int i= 0;i<3;i++)
        {
            this.gameObject.GetComponent<SkillList>().AddSkill(go);
        }
        
    }

}
