using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] List<string> objectives = new List<string>();

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveCount()
    {
        return objectives.Count;
    }

    public IEnumerable<string> GetObjectives()
    {
        return objectives;
    }

    public bool HasObjective(string objective)
    {
        return objectives.Contains(objective);
    }
}
