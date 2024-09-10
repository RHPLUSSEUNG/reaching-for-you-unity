using System.Threading;
using UnityEngine;

public class ObjectiveTracer : MonoBehaviour
{
    public static ObjectiveTracer Instance;

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

    ////KILL TYPE
    //public void ReportEnemyDefeated(Enemy _enemy)
    //{
    //    ObjectiveType objectveType = ObjectiveType.KILL;
    //    object target = monster.MonsterId;
    //    int successCount = 1;

    //    QuestManager.Instance.ReceiveReport(objectveType, target, successCount);
    //}

    ////GATHER TYPE
    //public void ReportItemPurchased(Item item)
    //{
    //    ObjectiveType objectveType = ObjectiveType.GATHER;
    //    object target = item.ItemId;
    //    int successCount = item.Quantity;

    //    QuestManager.Instance.ReceiveReport(objectveType, target, successCount);
    //}

    ////MOVE TYPE
    //public void ReportHarvestedCrop(Transform move)
    //{
        
    //}

    ////TALK TYPE
    //public void ReportTalkedToNPC(NPC npc)
    //{
    //    ObjectiveType objectveType = ObjectiveType.TALK;
    //    object target = npc.NPCId;
    //    int successCount = 1;

    //    QuestManager.Instance.ReceiveReport(objectveType, target, successCount);
    //}
}
