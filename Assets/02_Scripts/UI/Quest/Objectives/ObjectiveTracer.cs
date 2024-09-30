using System.Threading;
using UnityEngine;

public class ObjectiveTracer : MonoBehaviour
{
    public static ObjectiveTracer Instance;
    QuestList questList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
    }

    //[TODO:LSH][Require] ID of each Object

    //KILL TYPE
    public void ReportEnemyKilled(TestQuestEnemy _enemy)
    {
        ObjectiveType objectveType = ObjectiveType.KILL;
        int target = _enemy.targetId;
        int count = 1;

        questList.ReceiveReport(objectveType, target, count);
    }

    //GATHER TYPE
    public void ReportIItemCollected(int _itemID)
    {
        ObjectiveType objectveType = ObjectiveType.GATHER;
        int target = _itemID; //_item.ItemId;
        int count = Managers.Item.SearchItem(_itemID);

        questList.ReceiveReport(objectveType, target, count);
    }

    //MOVE TYPE
    public void ReportDestinationArrived(string _destination)
    {
        ObjectiveType objectveType = ObjectiveType.MOVE;
        string destination = _destination;

        questList.ReceiveReport(objectveType, destination);
    }

    //TALK TYPE (Last Objective)
    public void ReportNPCTalked(string _npcName)
    {
        ObjectiveType objectveType = ObjectiveType.TALK;
        string npcName = _npcName;

        questList.ReceiveReport(objectveType, npcName);
    }

    //MOVE TYPE
    public void ReportMovedToNPC(string _npcName)
    {
        ObjectiveType objectveType = ObjectiveType.MOVE;
        string npcName = _npcName;

        questList.ReceiveReport(objectveType, npcName);
    }
}
