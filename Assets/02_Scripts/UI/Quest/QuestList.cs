using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestPredicates
{
    NONE,
    HASQUEST,
    COMPLETEDQUEST,
    COMPLETEDOBJECTIVE,
    HASKILLED,
    HASGATHERED,
    HASMOVED,
    HASTALKED,
}

public class QuestList : MonoBehaviour, IPredicateEvaluator
{
    public static QuestList Instance;
    List<QuestStatus> statuses = new List<QuestStatus>();    

    public event Action onUpdate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddQuest(Quest quest)
    {
        if (HasQuest(quest))
        {
            return;
        }

        QuestStatus newStatus = new QuestStatus(quest);
        statuses.Add(newStatus);

        if (onUpdate != null)
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
        return GetQuestStatus(quest) != null;
    }

    public IEnumerable<QuestStatus> GetStatuses()
    {
        return statuses;
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        QuestStatus status = GetQuestStatus(quest);
        status.CompleteObjective(objective);

        if (status.IsComplete())
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

    //[TODO:LSH][Require] Inventory System
    void GiveReward(Quest quest)
    {
        foreach (var reward in quest.GetRewards())
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

    //Count Type
    public void ReceiveReport(ObjectiveType _objectiveType, int targetID, int count)
    {
        for (int i = 0; i < statuses.Count; i++)
        {            
            Quest quest = statuses[i].GetQuest();

            if (quest.GetQuestType() == QuestType.COMPLETED_QUEST)
            {
                continue;
            }

            switch (_objectiveType)
            {
                case ObjectiveType.KILL:
                    {
                        ObjectiveKillType objective = quest.GetObjective(_objectiveType) as ObjectiveKillType;

                        if (objective == null)
                        {
                            continue;
                        }

                        if (objective.GetTargetID() == targetID && !objective.IsComplete())
                        {
                            objective.ReiceiveKillCount(count);

                            if (objective.IsComplete())
                            {
                                //If receive reward through NPC, Change this code to "status.CompleteObjective(objective);"
                                CompleteObjective(quest, objective.GetReference());

                                onUpdate?.Invoke();
                            }
                        }                        
                        break;
                    }
                case ObjectiveType.GATHER:
                    {
                        ObjectiveGatherType objective = quest.GetObjective(_objectiveType) as ObjectiveGatherType;

                        if (objective == null)
                        {
                            continue;

                        }
                        if (objective.GetTargetID() == targetID)
                        {
                            objective.ReiceiveItemCount(count);
                            onUpdate?.Invoke();

                            if (objective.IsComplete())
                            {
                                //If receive reward through NPC, Change this code to "status.CompleteObjective(objective);"
                                CompleteObjective(quest, objective.GetReference());

                                if (statuses[i].IsComplete())
                                {
                                    onUpdate?.Invoke();
                                }
                            }
                            else
                            {
                                if(statuses[i].IsObjectiveComplete(objective.GetReference()))
                                {
                                    statuses[i].InCompleteObjective(objective.GetReference());
                                    onUpdate?.Invoke();
                                }
                            }
                                                        
                        }
                        break;
                    }
            }
        }
    }

    //Name Type
    public void ReceiveReport(ObjectiveType _objectiveType, string name)
    {
        for (int i = 0; i < statuses.Count; i++)
        {
            Quest quest = statuses[i].GetQuest();

            if(quest.GetQuestType() == QuestType.COMPLETED_QUEST)
            {
                continue;
            }

            switch (_objectiveType)
            {
                case ObjectiveType.TALK:
                    {
                        ObjectiveTalkType objective = quest.GetObjective(_objectiveType) as ObjectiveTalkType;
                        
                        if (objective == null)
                        {
                            continue;
                        }

                        objective.ReceiveNPCName(name);

                        if (objective.IsComplete() && statuses[i].GetisTimeToTalk())
                        {
                            CompleteObjective(quest, objective.GetReference());
                            onUpdate?.Invoke();

                            if (statuses[i].IsComplete())
                            {
                                quest.SetQuestType(QuestType.COMPLETED_QUEST);
                                onUpdate?.Invoke();                                
                            }
                        }

                        break;
                    }
                case ObjectiveType.MOVE:
                    {
                        ObjectiveMoveType objective = quest.GetObjective(_objectiveType) as ObjectiveMoveType;
                        
                        if (objective == null)
                        {
                            continue;
                        }

                        objective.ReceiveNPCName(name);

                        if (objective.IsComplete())
                        {
                            CompleteObjective(quest, objective.GetReference());
                            onUpdate?.Invoke();
                        }

                        break;
                    }
            }

        }
    }

    #region RequireSaveSystem
    //[TODO:LSH][Require] Save System
    public object CaptureState()
    {
        List<object> state = new List<object>();

        foreach (QuestStatus status in statuses)
        {
            state.Add(status.CaptureState());
        }

        return state;
    }

    public void RestoreState(object state)
    {
        List<object> stateList = state as List<object>;

        if (stateList == null)
        {
            return;
        }

        statuses.Clear();

        foreach (object objectState in stateList)
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
                    if (HasQuest(Quest.GetByName(parameters[0])))
                    {
                        return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                    }
                    else
                    {
                        return false;
                    }
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
