using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Gather]", menuName = "Quest / Objectives[Gather]", order = 2)]
public class ObjectiveGatherType : Objective
{
    [SerializeField] int targetID;
    [SerializeField] int targetCount;
    [SerializeField] int count = 0;

    public int GetTargetID()
    {
        return targetID;
    }

    public void ReiceiveItemCount(int _count)
    {
        count += _count;
    }

    public int GetItemCount()
    {
        return count;
    }

    public int GetTargetCount()
    {
        return targetCount;
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
