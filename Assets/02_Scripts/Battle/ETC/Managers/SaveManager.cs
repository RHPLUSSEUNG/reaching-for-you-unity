using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data
{
    #region item
    public Dictionary<int, int> consumeInven;
    public List<int> armorInven;
    public List<int> weaponInven;
    public int gold;
    #endregion

    #region player
    public List<QuestStatus> quests;
    public List<AMCharacterData> friends;
    public EntityStat charStat;
    public Equip_Item equipments;
    #endregion

    #region Adventrue
    public int stageCount;
    #endregion
}

public class SaveManager
{
    #region Attribute
    Data data = new Data();
    string path  = Application.dataPath + "/";
    string filename = "savedata";
    #endregion
    #region Data Function
    public void GetData()
    {
        data.consumeInven = Managers.Item.consumeInven;
        data.weaponInven = Managers.Item.weaponInven;
        data.armorInven= Managers.Item.armorInven;
        data.gold = Managers.Item.GetGold();

        data.quests = QuestList.Instance.GetQuestStatuses();
        data.friends = FriendshipManager.Instance.GetFriends();
        
        if (LoadSceneManager.sceneType == SceneType.PM_COMBAT)
        {
            EntityStat stat = GameObject.Find("Player_Girl_Battle(Clone)").GetComponent<EntityStat>();
            SetStat(data.charStat, stat);
            Equip_Item item = GameObject.Find("Player_Girl_Battle(Clone)").GetComponent<Equip_Item>();
            SetEquipItem(data.equipments, item);
        }
        else if (LoadSceneManager.sceneType == SceneType.ACADEMY || LoadSceneManager.sceneType == SceneType.PM_ADVENTURE)
        {
            EntityStat stat = GameObject.Find("Player_Girl").GetComponent<EntityStat>();
            SetStat(data.charStat, stat);
            Equip_Item item = GameObject.Find("Player_Girl").GetComponent<Equip_Item>();
            SetEquipItem(data.equipments, item);
        }

        data.stageCount = AdventureManager.StageCount;
    }

    public void SetData()
    {
        Managers.Item.consumeInven = data.consumeInven;
        Managers.Item.weaponInven = data.weaponInven;
        Managers.Item.armorInven = data.armorInven;
        Managers.Item.SetGold(data.gold);

        QuestList.Instance.SetQuestStatuses(data.quests);
        FriendshipManager.Instance.SetFriends(data.friends);

        if (LoadSceneManager.sceneType == SceneType.PM_COMBAT)
        {
            EntityStat stat = GameObject.Find("Player_Girl_Battle(Clone)").GetComponent<EntityStat>();
            SetStat(stat, data.charStat);
            Equip_Item item = GameObject.Find("Player_Girl_Battle(Clone)").GetComponent<Equip_Item>();
            SetEquipItem(item, data.equipments);
        }
        else if (LoadSceneManager.sceneType == SceneType.ACADEMY || LoadSceneManager.sceneType == SceneType.PM_ADVENTURE)
        {
            EntityStat stat = GameObject.Find("Player_Girl").GetComponent<EntityStat>();
            SetStat(stat, data.charStat);
            Equip_Item item = GameObject.Find("Player_Girl").GetComponent<Equip_Item>();
            SetEquipItem(item, data.equipments);
        }

        AdventureManager.StageCount = data.stageCount;
    }

    public Data ReturnData()
    {
        return data;
    }
    #endregion
    #region Game Save - Load Json
    public void SaveGame()
    {
        GetData();
        string data = JsonUtility.ToJson(this.data);
        File.WriteAllText(path + filename, data);
        Debug.Log(path);
    }

    public void LoadGame()
    {
        SceneChanger.Instance.ChangeScene(SceneType.ACADEMY);
        string data = File.ReadAllText(path + filename);
        this.data = JsonUtility.FromJson<Data>(data);
        SetData();
    }
    #endregion

    #region SetCharacterData
    private void SetStat(EntityStat stat, EntityStat saveStat)
    {
        if (stat == null || saveStat == null)
        {
            return;
        }
        stat.Level = saveStat.Level;
        stat.Hp = saveStat.Hp;
        stat.MaxHp = saveStat.MaxHp;
        stat.Mp = saveStat.Mp;
        stat.MaxMp = saveStat.MaxMp;
        stat.MovePoint = saveStat.MovePoint;
        stat.ActPoint = saveStat.ActPoint;
        stat.MaxMovePoint = saveStat.MaxMovePoint;
        stat.Defense = saveStat.Defense;
        stat.BaseDamage = saveStat.BaseDamage;
        stat.AttackRange = saveStat.AttackRange;

    }
    private void SetEquipItem(Equip_Item item, Equip_Item saveitem)
    {
        if (item == null || saveitem == null)
        {
            return;
        }
        item.Head = saveitem.Head;
        item.Weapon = saveitem.Weapon;
    }
    #endregion
}
