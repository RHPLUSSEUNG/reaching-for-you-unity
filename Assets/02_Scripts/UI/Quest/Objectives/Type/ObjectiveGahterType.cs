using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Gather]", menuName = "Quest / Objectives[Gather]", order = 2)]
public class ObjectiveGatherType : Objective
{
    [SerializeField] int targetID;
    [SerializeField] int targetCount;
    int count = 0;

    public int GetTargetID()
    {
        return targetID;
    }

    public void ReiceiveItem(int _itemID, int _objectCount)
    {
        if(targetID == _itemID)
        {
            //[TODO:LSH] gatherToComplete == player's ItemCountCheck(int _objectID);
            // IsAchieved();
        }
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
