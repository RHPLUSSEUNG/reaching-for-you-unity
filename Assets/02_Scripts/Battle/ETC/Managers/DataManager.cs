using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    List<ItemData> itemList;
    List<SkillData> skillList;

    public void OnAwake()
    {
        DataList data = GameObject.Find("DataList").GetComponent<DataList>();
        itemList = data.itemList;
        skillList = data.skillList;
    }

    public void SetItem(int itemid, Consume item)
    {
        item.name = itemList[itemid].name;
        item.type = itemList[itemid].ItemType;
        item.reqLev = itemList[itemid].reqLev;
    }

    public void SetItem(int itemid, Equipment item)
    {
        item.itemId = itemList[itemid].ItemID;
        item.name = itemList[itemid].ItemName;
        item.type = itemList[itemid].ItemType;
        item.reqLev = itemList[itemid].reqLev;

        item.part = itemList[itemid].part;
        item.elementType = itemList[itemid].element;
    }

    public void SetSkill(int skillid, Passive skill)
    {
        skill.skillName = skillList[skillid].name;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
    }

    public void SetSkill(int skillid, Active skill)
    {
        Debug.Log($"{skill} Setting");
        skill.skillName = skillList[skillid].name;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
        skill.stamina = skillList[skillid].stamina;
        skill.mp = skillList[skillid].mp;
    }

    public void SetSkill(int skillid, Travel skill)
    {
        skill.skillName = skillList[skillid].name;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
    }
}
