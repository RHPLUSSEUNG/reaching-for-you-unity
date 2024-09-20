using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists inside an NPC object
public class QuestHandler : MonoBehaviour
{    
    [SerializeField] Quest questToGive;
    [SerializeField] Quest questToComplete;
    [SerializeField] string objective;
    bool plzLookAtMe = false;

    private void Start()
    {
        if(questToGive != null)
        {
            plzLookAtMe = true;
        }
    }

    public void GiveQuest()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();
        questList.AddQuest(questToGive);
        plzLookAtMe = false;
    }    

    public void CompleteObjective()
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<QuestList>();        
        questList.CompleteObjective(questToComplete, objective);
    }

    public bool GetPlzLookAtMe()
    {
        return plzLookAtMe;
    }

    public void SetPlzLookAtMe(bool _plzLookAtMe)
    {
        plzLookAtMe = true;
    }
}
