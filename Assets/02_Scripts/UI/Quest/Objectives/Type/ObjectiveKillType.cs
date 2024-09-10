using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Kill]", menuName = "Quest / Objectives[Kill]", order = 1)]
public class ObjectiveKillType : Objective
{
    [SerializeField] int targetID;

    public int GetTargetID()
    {
        return targetID;
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