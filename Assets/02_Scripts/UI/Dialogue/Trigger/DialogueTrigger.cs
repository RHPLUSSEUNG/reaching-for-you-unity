using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Events;

public enum TriggerActionList
{
    NONE,
    GIVE_QUEST,
    GIVE_ITEM,
    COMPLETE_QUEST,
    COMPLETE_OBJECTIVE,
    REQUEST_ITEM,
    JOIN_TO_FRIEND,
    INCREASE_FRIENDSHIP,
    CHANGE_SCENE,
}


[System.Serializable]
struct TriggerStruct
{
    [SerializeField] public TriggerActionList action;
    [SerializeField] public UnityEvent onTrigger;
}

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TriggerStruct[] triggerStruct;

    public void Trigger(TriggerActionList actionToTrigger)
    {        
        foreach (TriggerStruct triggerStruct in triggerStruct) 
        {
            if (actionToTrigger == triggerStruct.action)
            {
                triggerStruct.onTrigger.Invoke();
            }
        }        
    }
}
