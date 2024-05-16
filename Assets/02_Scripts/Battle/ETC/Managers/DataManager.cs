using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    List<ItemData> itemList;
    List<SkillData> skillList;

    int compareItem(ItemData item1, ItemData item2)
    {
        return item1.ItemID < item2.ItemID ? -1 : 1;
    }

    int compareSkill(SkillData skill1, SkillData skill2)
    {
        return skill1.SkillId < skill2.SkillId ? -1 : 1;
    }

    public void OnAwake()
    {
        DataList data = Managers.Manager.gameObject.GetComponent<DataList>();
        itemList = data.itemList;
        skillList = data.skillList;
        Debug.Log(skillList);
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
        Debug.Log(skill);
        Debug.Log(skillid);
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
