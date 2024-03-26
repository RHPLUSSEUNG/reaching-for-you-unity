using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : Skill
{
    public abstract bool Activate();
    public abstract bool UnActivate();
}
