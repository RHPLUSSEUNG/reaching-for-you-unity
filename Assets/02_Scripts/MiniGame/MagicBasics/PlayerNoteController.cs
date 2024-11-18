using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoteController : MonoBehaviour
{
    TimingManager timingManager;

    void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            timingManager.CheckTiming();
        }
    }
}
