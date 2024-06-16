using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target, 30, element);
        return true;
    }
}
