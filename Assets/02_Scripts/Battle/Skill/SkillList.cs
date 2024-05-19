using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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

    public void AddDebuff(Debuff debuff)
    {
        Debuff cur = FindDebuff(debuff);
        if(cur != null)
        {
            cur.count++;
        }
        debuffs.Add(debuff);
    }

    public void DelDebuff(Debuff debuff)
    {
        debuffs.Remove(debuff);
    }

    public void CalcTurn()
    {
        if(debuffs.Count > 0)
        {
            foreach(Debuff debuff in debuffs)
            {
                debuff.TimeCheck();
            }
        }
    }

    public Debuff FindDebuff(Debuff debuff)
    {
        if (debuff == null) return null;
        foreach(Debuff de in debuffs)
        {
            if (de.GetType() == typeof(Debuff))
                return de;
        }
        return null;
    }
}
