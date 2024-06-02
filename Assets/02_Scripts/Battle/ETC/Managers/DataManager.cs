using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    List<ConsumeData> consumeList;
    List<EquipmentData> equipmentList;
    List<SkillData> skillList;
    
    public void OnAwake()
    {
        DataList data = GameObject.Find("DataList").GetComponent<DataList>();
        consumeList = data.consumeList;
        equipmentList = data.equipmentList;
        skillList = data.skillList;
    }

    public SkillData GetSkillData(int skillid)
    {
        return skillList[skillid];
    }

    public ItemData GetItemData(int itemid, bool equipment = false)
    {
        if (equipment)
        {
            return equipmentList[itemid];
        }
        else
            return consumeList[itemid];
    }

    public void SetItem(int itemid, Consume item)
    {
        item.name = consumeList[itemid].name;
        item.type = consumeList[itemid].ItemType;
        item.reqLev = consumeList[itemid].reqLev;
        item.target = consumeList[itemid].target;
        item.maxCapacity = consumeList[itemid].maxCapacity;
    }

    public void SetItem(int itemid, Equipment item)
    {
        item.itemId = equipmentList[itemid].ItemID;
        item.name = equipmentList[itemid].ItemName;
        item.type = equipmentList[itemid].ItemType;
        item.reqLev = equipmentList[itemid].reqLev;

        item.part = equipmentList[itemid].part;
        item.elementType = equipmentList[itemid].element;
    }

    public void SetSkill(int skillid, Passive skill)
    {
        skill.skillName = skillList[skillid].SkillName;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
    }

    public void SetSkill(int skillid, Active skill)
    {
        Debug.Log($"{skill} Setting");
        skill.skillName = skillList[skillid].SkillName;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
        skill.stamina = skillList[skillid].stamina;
        skill.mp = skillList[skillid].mp;
        skill.range = skillList[skillid].range;
        skill.target_object = skillList[skillid].target;
    }

    public void SetSkill(int skillid, Travel skill)
    {
        skill.skillName = skillList[skillid].name;
        skill.type = skillList[skillid].SkillType;
        skill.element = skillList[skillid].element;
    }
}
