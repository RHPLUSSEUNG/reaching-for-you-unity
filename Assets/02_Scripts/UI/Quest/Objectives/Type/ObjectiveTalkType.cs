using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Talk]", menuName = "Quest / Objectives[Talk]", order = 5)]
public class ObjectiveTalkType : Objective
{
    [SerializeField] string npcName;
    [SerializeField] bool talkToComplete;
    
    public void ReiceiveNPCName(string _npcName)
    {
        if (npcName == _npcName)
        {
            talkToComplete = true;
        }
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

