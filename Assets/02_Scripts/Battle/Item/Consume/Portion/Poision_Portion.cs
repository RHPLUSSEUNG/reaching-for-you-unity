using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poision_Portion : Consume
{
    public override bool Activate(GameObject target)
    {
        Poision poision = new();
        poision.SetDebuff(3, target, 10);
        return true;
    }
}
