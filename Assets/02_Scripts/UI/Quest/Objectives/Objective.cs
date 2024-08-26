using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    KILL,
    GATHER,
    TALK,
    MOVE,
    NONE
}

public abstract class Objective : ScriptableObject
{
    [SerializeField] ObjectiveType objectiveType;
    [SerializeField] protected string reference;
    [SerializeField] protected string description;

    public abstract bool IsAchieved();
    public abstract void UpdateObjective();
    public abstract void CompleteObjective();   

    public abstract string GetReference();
    public abstract string GetDescription();
}
