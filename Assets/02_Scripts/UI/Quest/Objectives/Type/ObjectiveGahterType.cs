using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Gather]", menuName = "Quest / Objectives[Gather]", order = 2)]
public class ObjectiveGatherType : Objective
{
    [SerializeField] int itemID;
    [SerializeField] int gatherToComplete;
    
    public void ReiceiveItem(int _itemID, int _objectCount)
    {
        if(itemID == _itemID)
        {
            //[TODO:LSH] gatherToComplete == player's ItemCountCheck(int _objectID);
            // IsAchieved();
        }
    }

    public override bool IsAchieved()
    {
        return true;
    }

    public override void UpdateObjective()
    {

    }

    public override void CompleteObjective()
    {

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
