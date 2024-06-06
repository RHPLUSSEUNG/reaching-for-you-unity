using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum QuestPredicates
{
    NONE,
    HASQUEST,
    COMPLETEDQUEST,
    COMPLETEDOBJECTIVE,
}

public class QuestList : MonoBehaviour, IPredicateEvaluator
{
    List<QuestStatus> statuses = new List<QuestStatus>();

    public event Action onUpdate;

    public void AddQuest(Quest quest)
    {
        if(HasQuest(quest))
        {
            return;
        }

        QuestStatus newStatus = new QuestStatus(quest);
        statuses.Add(newStatus);

        if(onUpdate != null)
        {
            onUpdate();
        }        
    }

    public bool HasQuest(Quest quest)
    {
        if (quest == null)
        {
            return false;
        }
        return GetQuestStatus(quest) !=  null;
    }

    public IEnumerable<QuestStatus> GetStatuses()
    {
        return statuses;
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        QuestStatus status = GetQuestStatus(quest);
        status.CompleteObjective(objective);
        
        if(status.IsComplete())
        {
            GiveReward(quest);
        }

        if (onUpdate != null)
        {
            onUpdate();
        }
    }    

    QuestStatus GetQuestStatus(Quest quest)
    {
        foreach (QuestStatus status in statuses)
        {
            if (status.GetQuest() == quest)
            {
                return status;
            }
        }
        return null;
    }

    //[Require] Inventory System
    void GiveReward(Quest quest)
    {
        foreach(var reward in quest.GetRewards()) 
        {
            //GetComponent<Inventory>.AddItem(reward.item, reward.number);
            //
            //If finite Inventory
            //
            //bool success = GetComponent<Inventory>.AddItem(reward.item, reward.number);
            //
            //if(!success)
            //{
            //    //Player should drop or delete some of the item
            //}
        }
    }

    #region RequireSaveSystem
    //[Require] Save System
    public object CaptureState()
    {
        List<object> state = new List<object>();

        foreach(QuestStatus status in statuses)
        {
            state.Add(status.CaptureState());
        }

        return state;
    }

    public void RestoreState(object state)
    {
        List<object> stateList = state as List<object>;

        if(stateList == null)
        {
            return;
        }

        statuses.Clear();

        foreach(object objectState in stateList)
        {
            statuses.Add(new QuestStatus(objectState));            
        }
    }
    #endregion


    public bool? Evaluate(QuestPredicates predicate, string[] parameters)
    {
        switch (predicate)
        {
            case QuestPredicates.HASQUEST:
                {                    
                    return HasQuest(Quest.GetByName(parameters[0]));
                }                
            case QuestPredicates.COMPLETEDQUEST:
                {
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                }                
            case QuestPredicates.COMPLETEDOBJECTIVE:
                {
                    Quest quest = Quest.GetByName(parameters[0]);
                    QuestStatus status = GetQuestStatus(quest);

                    if (status == null)
                    {
                        return false;
                    }

                    return status.IsObjectiveComplete(parameters[1]);
                }                
            }

            return null;
        }
}
