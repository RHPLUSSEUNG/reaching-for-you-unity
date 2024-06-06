using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Transform mainQuestContent;
    [SerializeField] Transform subQuestContent;
    [SerializeField] Transform completedQuestContent;
    [SerializeField] QuestItemUI questPrefab;
    [SerializeField] GameObject emptyText;
    QuestList questList;

    private void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {        
        foreach (Transform item in mainQuestContent)
        {            
            Destroy(item.gameObject);
        }
        foreach (Transform item in subQuestContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in completedQuestContent)
        {
            Destroy(item.gameObject);
        }

        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance;

            switch (status.GetQuest().GetQuestType())
            {
                case QuestType.MAIN_QUEST:
                    {
                        uiInstance = Instantiate<QuestItemUI>(questPrefab, mainQuestContent);
                        break;
                    }
                case QuestType.SUB_QUEST:
                    {
                         uiInstance = Instantiate<QuestItemUI>(questPrefab, subQuestContent);
                        break;
                    }
                case QuestType.COMPLETED_QUEST:
                    {
                        uiInstance = Instantiate<QuestItemUI>(questPrefab, completedQuestContent);
                        break;
                    }
                default:
                    {
                        uiInstance = null;
                        break;
                    }
            }
            
            uiInstance.Setup(status);
        }
    }
}
