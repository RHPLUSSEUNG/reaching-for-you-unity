using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
struct TriggerStruct
{
    [SerializeField] public string action;
    [SerializeField] public UnityEvent onTrigger;
}

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TriggerStruct[] triggerStruct;

    public void Trigger(string actionToTrigger)
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
