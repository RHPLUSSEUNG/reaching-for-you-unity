using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public List<GameObject> list = new();
    public List<Buff> buffs = new();
    public List<Debuff> debuffs = new();
    int count = 5;
    public void AddSkill(GameObject skill)
    {
        if (skill == null || list.Count == count)
        {
            return;
        }
        list.Add(skill);

        if(skill.GetComponent<Passive>()!= null)
        {
            skill.GetComponent<Passive>().Activate(this.gameObject);
        }
    }

    public void RemoveSkill(GameObject skill) 
    {
        if(skill == null || list.Count == 0)
        {
            return;
        }

        list.Remove(skill);

        if (skill.GetComponent<Passive>() != null)
        {
            skill.GetComponent<Passive>().UnActivate(this.gameObject);
        }
    }


}
