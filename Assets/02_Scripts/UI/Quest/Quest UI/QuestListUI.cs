using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{    
    [SerializeField] QuestItemUI questPrefab;
    QuestList questList;

    private void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(status);
        }
    }
}
