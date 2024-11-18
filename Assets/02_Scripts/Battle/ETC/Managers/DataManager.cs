using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    public void OnAwake()
    {
        DataList data = GameObject.Find("DataList").GetComponent<DataList>();
        consumeList = data.consumeList;
        playerSkillList = data.playerSkillList;
        monsterSkillList = data.monsterSkillList;
        characterList = data.characterList;
        NPCList = data.npcList;
        monsterList = data.monsterList;
        weaponList = data.weaponList;
        armorList = data.armorList;
        Debug.Log("Data Loading Complete");
    }

    #region Map & Monster
    private readonly Dictionary<MapList, Type> MapMonsterList = new () 
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
    List<EquipmentData> equipmentList;
    List<SkillData> playerSkillList;
    List<SkillData> monsterSkillList;

    List<CharacterData> characterList;
    List<AMNPCData> NPCList;
    List<MonsterData> monsterList;
    public List<EquipmentData> weaponList;
    public List<EquipmentData> armorList;
    List<ConsumeData> consumeList;

    public ScriptableObject ParsingData(int id)
    {
        int idx = id % 1000;
        return (id / 1000) switch
        {
            1 => GetCharacterData(idx),
            2 => GetNPCData(idx),
            3 => GetMonsterData(idx),
            4 => GetWeaponData(idx),
            5 => GetArmorData(idx),
            6 => GetConsumeData(idx),
            _ => null,
        };
    }

    public EquipmentData GetWeaponData(int id)
    {
        return weaponList[id];
    }
    public EquipmentData GetArmorData(int id)
    {
        return armorList[id];
    }

    public CharacterData GetCharacterData(int characterId)
    {
        return characterList[characterId];
    }

    public AMNPCData GetNPCData(int npcId)
    {
        return NPCList[npcId];
    }
    public MonsterData GetMonsterData(int monsterId)
    {
        return monsterList[monsterId];
    }
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
    public ItemData GetConsumeData(int itemid)
    {
        return consumeList[itemid];
    }
    public void SetItem(int itemid, Consume item)
    {
        ConsumeData info = (ConsumeData)ParsingData(itemid);
        item.name = info.name;
        item.type = info.ItemType;
        item.reqLev = info.reqLev;
        item.targetObject = info.target;
        item.maxCapacity = info.maxCapacity;
    }

    public void SetItem(int itemid, Equipment item)
    {
        EquipmentData info = (EquipmentData)ParsingData(itemid);
        item.itemId = info.ItemID;
        item.name = info.ItemName;
        item.type = info.ItemType;
        item.reqLev = info.reqLev;

        item.part = info.part;
        item.elementType = info.element;
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

    public void SetTravelSkill(int skillid, Travel skill)
    {
        skill.skillName = playerSkillList[skillid].name;
        skill.type = playerSkillList[skillid].SkillType;
        skill.element = playerSkillList[skillid].element;
    }
    #endregion
}
