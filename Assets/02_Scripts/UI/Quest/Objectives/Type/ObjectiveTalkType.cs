using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Talk]", menuName = "Quest / Objectives[Talk]", order = 5)]
public class ObjectiveTalkType : Objective
{
    [SerializeField] string npcName;
    [SerializeField] bool talkToComplete = false;
    
    public void ReceiveNPCName(string _npcName)
    {
        if (npcName == _npcName)
        {
            talkToComplete = true;
        }
    }
    
    public string GetNPCName()
    {
        return npcName;
    }
    
    public override bool IsComplete()
    {
        return talkToComplete;
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

