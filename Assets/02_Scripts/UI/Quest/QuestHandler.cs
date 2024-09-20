using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists inside an NPC object
public class QuestHandler : MonoBehaviour
{    
    [SerializeField] Quest questToGive;
    [SerializeField] Quest questToComplete;
    [SerializeField] string objective;
    bool isQuestGiven = false;

    private void Start()
    {
        if(questToGive != null)
        {            
            isQuestGiven = true;
        }
    }

    public void GiveQuest()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.AddQuest(questToGive);
        isQuestGiven = false;
    }    

    public void CompleteObjective()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();        
        questList.CompleteObjective(questToComplete, objective);
    }

    public bool GetIsQuestGiven()
    {
        return isQuestGiven;
    }
}
