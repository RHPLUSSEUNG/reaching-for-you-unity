using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public void OnAwake()
    {
        DataList data = GameObject.Find("DataList").GetComponent<DataList>();
        consumeList = data.consumeList;
        equipmentList = data.equipmentList;
        playerSkillList = data.playerSkillList;
        monsterSkillList = data.monsterSkillList;
    }

    #region Map & Monster
    Dictionary<MapList, Type> MapMonsterList = new Dictionary<MapList, Type>() 
    {
        { MapList.DessertMonster, typeof(DessertMonster) }
    };

    public Array GetMonsterList(MapList map)
    {
        if(MapMonsterList.TryGetValue(map, out Type enumType))
        {
            return Enum.GetValues(enumType);
        }
        return null;
    }



    #endregion

    #region Skill & Item
    List<ConsumeData> consumeList;
    List<EquipmentData> equipmentList;
    List<SkillData> playerSkillList;
    List<SkillData> monsterSkillList;

   
    public SkillData GetPlayerSkillData(int skillid)
    {
        return playerSkillList[skillid];
    }

    public SkillData GetMonsterSkillData(int skillid)
    {
        return monsterSkillList[skillid];
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

    public void SetPlayerSkill(int skillid, Passive skill)
    {
        skill.skillName = playerSkillList[skillid].SkillName;
        skill.type = playerSkillList[skillid].SkillType;
        skill.element = playerSkillList[skillid].element;
    }

    public void SetPlayerSkill(int skillid, Active skill)
    {
        //Debug.Log($"{skill} Setting");
        skill.skillName = playerSkillList[skillid].SkillName;
        skill.type = playerSkillList[skillid].SkillType;
        skill.element = playerSkillList[skillid].element;
        skill.stamina = playerSkillList[skillid].stamina;
        skill.mp = playerSkillList[skillid].mp;
        skill.range = playerSkillList[skillid].range;
        skill.target_object = playerSkillList[skillid].target;
    }

    public void SetMonsterSkill(int skillid, MonsterSkill skill)
    {
        //Debug.Log($"{skill} Setting");
        skill.skillName = monsterSkillList[skillid].SkillName;
        skill.type = monsterSkillList[skillid].SkillType;
        skill.element = monsterSkillList[skillid].element;
        skill.stamina = monsterSkillList[skillid].stamina;
        skill.mp = monsterSkillList[skillid].mp;
        skill.range = monsterSkillList[skillid].range;
        skill.target_object = monsterSkillList[skillid].target;
    }

    public void SetSkill(int skillid, Travel skill)
    {
        skill.skillName = playerSkillList[skillid].name;
        skill.type = playerSkillList[skillid].SkillType;
        skill.element = playerSkillList[skillid].element;
    }
    #endregion
}
