using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    NONE,
    MAIN_QUEST,
    SUB_QUEST,
    COMPLETED_QUEST,
}

[CreateAssetMenu(fileName = "Quests", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] QuestType questType;
    [SerializeField] List<Objective> objectives = new List<Objective>();
    [SerializeField] List<Reward> rewards = new List<Reward>();    

    [System.Serializable]
    public class Reward
    {
        [Min(1)]
        public int number;
        public GameObject item;
    }

    [System.Serializable]
    public class Objective
    {
        public string reference;
        public string description;
    }

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveCount()
    {
        return objectives.Count;
    }

    public IEnumerable<Objective> GetObjectives()
    {
        return objectives;
    }

    public IEnumerable<Reward> GetRewards()
    {
        return rewards;
    }

    public bool HasObjective(string objectiveReference)
    {
        foreach(var objective in objectives)
        {
            if(objective.reference == objectiveReference)
            {
                return true;
            }
        }

        return false;
    }

    public static Quest GetByName(string questName)
    {
        foreach(Quest quest in Resources.LoadAll<Quest>("Quests"))
        {
            if (quest.name == questName)
            {
                return quest;
            }            
        }

        return null;
    }

    public QuestType GetQuestType()
    {
        return questType;
    }

    public void SetQuestType(QuestType _questType)
    {
        questType = _questType;
    }
}
