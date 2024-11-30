using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind_Portion : Consume
{
    public override bool Activate(GameObject target)
    {
        Bind bind = new();
        bind.SetDebuff(3, target);
        return true;
    }
}
