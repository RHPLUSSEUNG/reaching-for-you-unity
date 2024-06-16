using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTorture : Active
{
    public override bool Activate()
    {
        BubbleDebuff debuff = new();
        debuff.SetDebuff(1, target, 40);
        return true;
    }
}