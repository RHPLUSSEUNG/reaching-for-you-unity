using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Move]", menuName = "Quest / Objectives[Move]", order = 4)]
public class ObjectiveMoveType : Objective
{    
    [SerializeField] int moveToComplete;

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
