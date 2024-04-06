using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().remainStamina += 1;
        return true;
    }
}