using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestEnemy : MonoBehaviour
{    
    [SerializeField] int HP = 100;
    [SerializeField] public int targetId = 1;

    private void Update()
    {
        if (HP <= 0)
        {
            ObjectiveTracer.Instance.ReportEnemyKilled(this);
            HP = 100;            
        }
    }    

}
