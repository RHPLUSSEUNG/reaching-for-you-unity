using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Transform mainQuestContent;
    [SerializeField] Transform subQuestContent;
    [SerializeField] Transform completedQuestContent;
    [SerializeField] QuestItemUI questPrefab;
    [SerializeField] GameObject[] emptyText;    

    private void Start()
    {        
        QuestList.Instance.onUpdate += Redraw;
        Redraw();
    }
    private void OnDestroy()
    {
        QuestList.Instance.onUpdate -= Redraw;
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

        bool hasMainQuest = false;
        bool hasSubQuest = false;
        bool hasCompletedQuest = false;

        foreach (QuestStatus status in QuestList.Instance.GetStatuses())
        {
            QuestItemUI uiInstance;

            switch (status.GetQuest().GetQuestType())
            {
                case QuestType.MAIN_QUEST:
                    {
                        uiInstance = Instantiate<QuestItemUI>(questPrefab, mainQuestContent);
                        hasMainQuest = true;
                        break;
                    }
                case QuestType.SUB_QUEST:
                    {
                        uiInstance = Instantiate<QuestItemUI>(questPrefab, subQuestContent);
                        hasSubQuest = true;
                        break;
                    }
                case QuestType.COMPLETED_QUEST:
                    {
                        uiInstance = Instantiate<QuestItemUI>(questPrefab, completedQuestContent);
                        hasCompletedQuest = true;
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

        if (!hasMainQuest)
        {
            Instantiate(emptyText[0], mainQuestContent);
        }
        if (!hasSubQuest)
        {
            Instantiate(emptyText[1], subQuestContent);
        }
        if (!hasCompletedQuest)
        {
            Instantiate(emptyText[2], completedQuestContent);
        }
    }
}
