using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target, 30, element);
        Cold cold = new();
        cold.SetDebuff(99, target, 1);

        return true;
    }
}
