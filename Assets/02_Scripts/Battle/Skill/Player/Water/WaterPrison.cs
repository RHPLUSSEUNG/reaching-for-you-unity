using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaterPrison : Active
{
    public override bool Activate()
    {
        BubbleDebuff debuff = new();
        debuff.SetDebuff(3, target, 40);
        return true;
    }
}
