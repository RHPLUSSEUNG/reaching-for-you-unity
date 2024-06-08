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
        idList.Add(id);
    }

    public void RemoveSkill(int id)
    {
        idList.Remove(id);
    }

    public void AddSkill(bool monster)
    {
        foreach (int id in idList)
        {
            list.Add(Managers.Skill.InstantiateSkill(id, monster));
        }
    }

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
    #endregion

}