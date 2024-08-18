using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target, 50, ElementType.Fire);
        return true;
    }
}
