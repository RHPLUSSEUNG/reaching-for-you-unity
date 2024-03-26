using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Active : Skill
{
    public short cooltime;

    public abstract bool Activate();
    
}
