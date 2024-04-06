using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        IncreaseAtk buff = new IncreaseAtk();
        buff.SetBuff(3, target, 5);
        buff.Active();
        return true;
    }
}