using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().mp += 75;
        return true;
    }
}