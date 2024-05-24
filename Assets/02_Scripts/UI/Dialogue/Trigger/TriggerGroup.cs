using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroup : MonoBehaviour
{
    [SerializeField] GameObject[] relevantPerson;
    [SerializeField] bool activateOnStart = false;

    private void Start()
    {
        Activate(activateOnStart);
    }

    public void Activate(bool shouldActive)
    {
        foreach(GameObject _relevant in relevantPerson)
        {
            //[TODO]
            //_relevant.GetComponent<DialgoueTrigger>().Plus
        }
    }
}
