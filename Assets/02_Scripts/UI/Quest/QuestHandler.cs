using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] Quest questToGive;
    [SerializeField] Quest questToComplete;
    [SerializeField] string objective;

    public void GiveQuest()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.AddQuest(questToGive);
    }

    public void CompleteObjective()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.CompleteObjective(questToComplete, objective);
    }
}
