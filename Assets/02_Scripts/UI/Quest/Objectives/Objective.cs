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
    [SerializeField] protected ObjectiveType objectiveType;
    [SerializeField] protected string reference;
    [SerializeField] protected string description;
    
    public abstract bool IsComplete();
    public abstract ObjectiveType GetObjectiveType();
    public abstract string GetReference();
    public abstract string GetDescription();    
}
