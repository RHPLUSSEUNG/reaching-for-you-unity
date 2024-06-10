using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;

    public void GiveQuest()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.AddQuest(quest);
    }

}
