using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Active : Skill
{
    public short cooltime; //skill cool time
    bool[,] skillRange = new bool[11, 11]; //[6][6] activate pos
    public abstract bool Activate(Vector3 pos); //use skill
    public abstract bool SetPos(); //setting skill range
}
