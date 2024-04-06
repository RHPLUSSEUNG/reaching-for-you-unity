using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().stamina += 3;
        return true;
    }
}
