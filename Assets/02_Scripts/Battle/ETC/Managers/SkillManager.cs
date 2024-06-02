using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager
{
    Active skill;
    GameObject target;
    GameObject usingSkill;
    public GameObject extent;

    public void Awake()
    {
        usingSkill = GameObject.Find("UsingSkill");
        Debug.Log(usingSkill);
        extent = GameObject.Find("SkillExtent");
    }

    public GameObject Instantiate(int id)
    {
        GameObject TestSkill = Managers.Prefab.Instantiate($"Skill/{Managers.Data.GetSkillData(id).SkillName}", usingSkill.transform);
        Debug.Log(TestSkill);
        return TestSkill;
    }
}
