using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

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
    #endregion

    #region Adventrue
    public int stageCount;
    #endregion
}

public class SaveManager : MonoBehaviour
{
    public Data data = new Data();
    string path  = Application.dataPath + "/";
    string filename = "savedata";
    private void GetData()
    {
        data.consumeInven = Managers.Item.consumeInven;
        data.weaponInven = Managers.Item.weaponInven;
        data.armorInven= Managers.Item.armorInven;
        data.gold = Managers.Item.GetGold();

        data.quests = QuestList.Instance.GetQuestStatuses();

        data.stageCount = AdventureManager.StageCount;
    }

    public void SetData()
    {
        Managers.Item.consumeInven = data.consumeInven;
        Managers.Item.weaponInven = data.weaponInven;
        Managers.Item.armorInven = data.armorInven;
        Managers.Item.SetGold(data.gold);

        QuestList.Instance.SetQuestStatuses(data.quests);

        AdventureManager.StageCount = data.stageCount;
    }
    public void SaveGame()
    {
        GetData();
        string data = JsonUtility.ToJson(this.data);
        File.WriteAllText(path + filename, data);
        Debug.Log(path);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(path);
        string data = File.ReadAllText(path + filename);
        this.data = JsonUtility.FromJson<Data>(data);
        SetData();
    }
}
