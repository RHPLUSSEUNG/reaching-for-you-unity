using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public List<GameObject> list = new();
    
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

}