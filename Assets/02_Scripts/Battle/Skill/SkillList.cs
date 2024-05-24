using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public List<GameObject> list = new();
    public List<Buff> buffs = new();
    public List<Debuff> debuffs = new();

    private List<Buff> deleteBuffList = new();
    private List<Debuff> deleteDebuffList = new();
    int count = 5;

    #region Skill
    public void AddSkill(GameObject skill)
    {
        if (skill == null || list.Count == count)
        {
            return;
        }
        Debug.Log(skill);
        list.Add(skill);

        if (skill.GetComponent<Passive>() != null)
        {
            skill.GetComponent<Passive>().Activate(this.gameObject);
        }
    }

    public void RemoveSkill(GameObject skill)
    {
        if (skill == null || list.Count == 0)
        {
            return;
        }

        list.Remove(skill);

        if (skill.GetComponent<Passive>() != null)
        {
            skill.GetComponent<Passive>().UnActivate(this.gameObject);
        }
    }
    #endregion

    #region Buff & Debuff
    public void AddDebuff(Debuff debuff)
    {
        Debuff cur = FindDebuff(debuff);
        if (cur != null)
        {
            cur.Duplicate_Debuff(debuff);
        }
        else
        {
            debuffs.Add(debuff);
        }
    }

    public void DelDebuff(Debuff debuff)
    {
        deleteDebuffList.Add(debuff);
    }

    public void DelBuff(Buff buff)
    {
        deleteBuffList.Add(buff);
    }

    public void ClearBuff_Debuff()
    {
        foreach (Debuff debuff in deleteDebuffList)
        {
            debuffs.Remove(debuff);
        }

        foreach (Buff buff in deleteBuffList)
        {
            buffs.Remove(buff);
        }

        deleteBuffList.Clear();
        deleteDebuffList.Clear();
    }

    public void CalcTurn()
    {
        if (debuffs.Count > 0)
        {
            foreach (Debuff debuff in debuffs)
            {
                debuff.TimeCheck();
            }
        }

        if (buffs.Count > 0)
        {
            foreach (Buff buff in buffs)
            {
                buff.TimeCheck();
            }
        }

        ClearBuff_Debuff();
    }

    public Debuff FindDebuff(Debuff debuff)
    {
        if (debuff == null) return null;
        foreach (Debuff de in debuffs)
        {
            if (de.GetType() == typeof(Debuff))
                return de;
        }
        return null;
    }
    #endregion
}