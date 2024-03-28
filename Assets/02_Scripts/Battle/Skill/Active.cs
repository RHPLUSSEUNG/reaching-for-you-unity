using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Active : Skill
{
    public short cooltime; //skill cool time
    bool[,] skillRange = new bool[11, 11]; //[6][6] player position
    public abstract bool Activate(); //use skill
    public abstract bool SetRange(); //setting skill range
}
