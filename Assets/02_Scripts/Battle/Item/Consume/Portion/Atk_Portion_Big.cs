using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<CharacterSpec>().attack += 10;
        return true;
    }
}
