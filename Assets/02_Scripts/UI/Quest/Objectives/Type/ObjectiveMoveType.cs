using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Move]", menuName = "Quest / Objectives[Move]", order = 4)]
public class ObjectiveMoveType : Objective
{    
    [SerializeField] string destinationName;
    bool moveToComplete = false;

    public void ReceiveDestinationName(string _destinationName)
    {
        if (destinationName == _destinationName)
        {
            moveToComplete = true;
        }
    }

    public override bool IsComplete()
    {
        return moveToComplete;
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
