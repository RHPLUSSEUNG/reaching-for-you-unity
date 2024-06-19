using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public List<GameObject> list = new();
    public List<int> idList = new();

    int count = 5;

    #region Skill

    public void AddSkill(int id)
    {
        if (list.Count < count)
        {
            idList.Add(id);
        }
        else
        {
            Debug.Log("Max Skill Count");
        }
    }

    public void RemoveSkill(int id)
    {
        if(list.Count > 0)
        {
            idList.Remove(id);
        }
        else
        {
            Debug.Log("There is no equip skill");
        }
    }

    public void AddSkill(bool monster)
    {
        foreach (int id in idList)
        {
            AddSkill(Managers.Skill.InstantiateSkill(id, monster));
        }
    }

    public void AddSkill(GameObject skill)
    {
        if (skill == null || list.Count == count)
        {
            return;
        }
        //Debug.Log(skill);
        list.Add(skill);

        if (skill.GetComponent<Passive>() != null)
        {
            skill.GetComponent<Passive>().Activate(this.gameObject);
        }
    }
    #endregion
}