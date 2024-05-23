using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillManager
{
    Active skill;
    GameObject target;
    GameObject usingSkill;

    public void Awake()
    {
        usingSkill = GameObject.Find("UsingSkill");
        Debug.Log(usingSkill);
    }

    public void SetSkillRange(Active skill)
    {
        if(skill == null)
        {
            return;
        }
        Managers.raycast.detector.SetDetector(Managers.Battle.currentCharacter, skill.range, skill.target_object);
    }

    public void SkillAct(GameObject target)
    {
        skill.SetTarget(target);
    }

    public GameObject Instantiate(int id)
    {
        GameObject TestSkill = Managers.Prefab.Instantiate($"Skill/{Managers.Data.GetSkillData(id).SkillName}", usingSkill.transform);
        Debug.Log(TestSkill);
        return TestSkill;
    }
}
