using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective[Kill]", menuName = "Quest / Objectives[Kill]", order = 1)]
public class ObjectiveKillType : Objective
{
    //[TODO:LSH] [SerializeField] EnemyType enemyType;
    [SerializeField] int killsToComplete;

    public void ReiceiveEnemyName(int _enemyType, int _enemyCount)
    {
        //if(enemyType == _enemyType){}
        if (killsToComplete == _enemyCount)
        {
            //IsAchieved()
        }
    }

    public override bool IsAchieved()
    {
        return true;
    }

    public override void UpdateObjective()
    {

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