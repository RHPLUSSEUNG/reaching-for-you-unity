using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().shield += 5;
        return true;
    }
}