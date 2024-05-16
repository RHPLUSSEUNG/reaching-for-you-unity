using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Active : Skill
{
    public int stamina; //skill cool time
    public int mp;
    bool[,] skillRange = new bool[11, 11]; //[6][6] activate pos
    public abstract bool Activate(); //use skill
    public abstract bool SetTarget(GameObject target); //setting skill target

    public override void Start()
    {
        Managers.Data.SetSkill(skillId, this);
    }
}