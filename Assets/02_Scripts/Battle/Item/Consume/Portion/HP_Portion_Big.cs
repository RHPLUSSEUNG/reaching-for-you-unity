using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().hp += 100;
        return true;
    }
}
