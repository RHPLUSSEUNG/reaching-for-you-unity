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
    public void ReportEnemyKilled(GameObject _enemy)
    {
        ObjectiveType objectveType = ObjectiveType.KILL;
        int target = 12341234;// = _enemy.enemyID;
        int count = 1;

        questList.ReceiveReport(objectveType, target, count);
    }

    //GATHER TYPE
    public void ReportIItemCollected(GameObject _item, int _count)
    {
        ObjectiveType objectveType = ObjectiveType.GATHER;
        int target = 12341234; //_item.ItemId;
        int count = _count;

        questList.ReceiveReport(objectveType, target, count);
    }

    //MOVE TYPE
    public void ReportDestinationArrived(string _destination)
    {
        ObjectiveType objectveType = ObjectiveType.MOVE;
        string destination = _destination;

        questList.ReceiveReport(objectveType, destination);
    }

    //TALK TYPE
    public void ReportNPCTalked(string _npcName)
    {
        ObjectiveType objectveType = ObjectiveType.TALK;
        string npcName = _npcName;

        questList.ReceiveReport(objectveType, npcName);
    }
}
