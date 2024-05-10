using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;

        IncreaseShield buff = new IncreaseShield();
        buff.SetBuff(3, target, 5);
        buff.Active();
        return true;
    }
}