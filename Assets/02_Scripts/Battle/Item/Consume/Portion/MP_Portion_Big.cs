using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<EntityStat>().Mp += 100;
        return true;
    }
}
