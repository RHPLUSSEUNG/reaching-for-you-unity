using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Portion : Consume
{
    public override bool Activate(GameObject target)
    {
        Freeze freeze = new();
        freeze.SetDebuff(3, target);
        return true;
    }
}
