using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour
{
    [SerializeField]
    public List<CharacterData> characterList;
    public List<AMNPCData> npcList;
    public List<MonsterData> monsterList;
    public List<EquipmentData> weaponList;
    public List<EquipmentData> armorList;
    public List<ConsumeData> consumeList;



    public List<EquipmentData> equipmentList;
    public List<SkillData> playerSkillList;
    public List<SkillData> monsterSkillList;
}
