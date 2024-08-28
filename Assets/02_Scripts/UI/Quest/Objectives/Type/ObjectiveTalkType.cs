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

    public override bool IsAchieved()
    {
        return true;
    }

    public override void UpdateObjective()
    {
        if(talkToComplete)
        {
            CompleteObjective();
        }
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

