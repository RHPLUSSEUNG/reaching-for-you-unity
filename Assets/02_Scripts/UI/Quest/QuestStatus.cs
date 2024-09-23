using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    Quest quest;
    List<string> completedObjectives = new List<string>();
    bool isTimeToTalk = false;

    [System.Serializable]
    class QuestStatusRecord
    {
        public string questName;
        public List<string> completedObjectives;
    }


    public QuestStatus(Quest quest)
    {
        this.quest = quest;
    }

    public QuestStatus(object objectState)
    {
        QuestStatusRecord state = objectState as QuestStatusRecord;
        quest = Quest.GetByName(state.questName);
        completedObjectives = state.completedObjectives;

    }

    public Quest GetQuest()
    {
        return quest;
    }

    public bool IsComplete() 
    {
        foreach(var objective in quest.GetObjectives())
        {
            if(!completedObjectives.Contains(objective.GetReference()))
            {
                return false; 
            }
        }

        return true;
    }

    public int GetCompletedCount()
    {
        return completedObjectives.Count;
    }

    public bool IsObjectiveComplete(string objective)
    {
        return completedObjectives.Contains(objective);
    }

    public void CompleteObjective(string objective)
    {
        if(quest.HasObjective(objective) && !completedObjectives.Contains(objective))
        {
            completedObjectives.Add(objective);
        }

        ControlTimeToTalk();
    }

    public void InCompleteObjective(string objective)
    {
        if (quest.HasObjective(objective) && completedObjectives.Contains(objective))
        {
            completedObjectives.Remove(objective);
        }

        ControlTimeToTalk();
    }

    void ControlTimeToTalk()
    {
        //[LSH:TODO] Need Refactor
        if (quest.GetObjectiveCount() - 1 == completedObjectives.Count)
        {
            isTimeToTalk = true;
            ObjectiveTalkType objective = quest.GetObjective(ObjectiveType.TALK) as ObjectiveTalkType;
            GameObject TargetNPC = GameObject.Find(objective.GetNPCName());

            TargetNPC.transform.GetChild(1).GetComponent<QuestHandler>().SetPlzLookAtMe(true);
        }
        else
        {
            isTimeToTalk = false;
            ObjectiveTalkType objective = quest.GetObjective(ObjectiveType.TALK) as ObjectiveTalkType;
            GameObject TargetNPC = GameObject.Find(objective.GetNPCName());

            TargetNPC.transform.GetChild(1).GetComponent<QuestHandler>().SetPlzLookAtMe(false);
        }
    }

    public bool GetisTimeToTalk()
    {        
        return isTimeToTalk;
    }

    public void InCompleteObjective(string objective)
    {
        if (quest.HasObjective(objective) && completedObjectives.Contains(objective))
        {
            completedObjectives.Remove(objective);
        }
    }

    public object CaptureState()
    {
        QuestStatusRecord state = new QuestStatusRecord();
        state.questName = quest.name;
        state.completedObjectives = completedObjectives;

        return state;
    }
}
