using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should belong to the enemy object hierarchy
public class KillCounter : MonoBehaviour
{
    [SerializeField] string identifier;
    [SerializeField] bool onlyIfInitialized = true;

    ObjectiveCounter counter;

    private void Awake()
    {
        counter = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<ObjectiveCounter>();
        //GetComponent<Health>().onDeath.AddListener(AddtoCounter);        
    }

    void AddToCount()
    {
        counter.AddToCount(identifier, 1, onlyIfInitialized);
    }
}
