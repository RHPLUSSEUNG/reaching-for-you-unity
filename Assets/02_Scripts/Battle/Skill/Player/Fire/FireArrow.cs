using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFireArrow : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target, 50, ElementType.Fire);
        Managers.Active.Damage(target, 50, ElementType.Fire);
        Burn burn = new();
        burn.SetDebuff(5, target, 20);
        return true;
    }
}
