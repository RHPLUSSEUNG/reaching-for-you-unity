using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objective HUB
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    GameObject player;

    public Action OnCountChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void ManageKillTypeObjective(int _objectID)
    {
        
    }
}
