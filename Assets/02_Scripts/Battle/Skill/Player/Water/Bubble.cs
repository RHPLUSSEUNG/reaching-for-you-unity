using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target, 10, element, false);
        return true;
    }
}
