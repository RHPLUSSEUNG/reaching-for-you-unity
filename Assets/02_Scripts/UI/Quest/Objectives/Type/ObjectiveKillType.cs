using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Kill]", menuName = "Quest / Objectives[Kill]", order = 1)]
public class ObjectiveKillType : Objective
{
    [SerializeField] int targetID;
    [SerializeField] int targetCount;
    int count = 0;

    public int GetTargetID()
    {
        return targetID;
    }

    public void ReiceiveKillCount(int _count)
    {
        count += _count;
    }

    public override bool IsComplete()
    {
        return count >= targetCount;
    }

    public override ObjectiveType GetObjectiveType()
    {
        return objectiveType;
    }
    public override string GetReference()
    {
        return reference;
    }

    public override string GetDescription()
    {
        return description;
    }
}