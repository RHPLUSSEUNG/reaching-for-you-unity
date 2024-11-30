using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepenceDown : Active
{
    public override bool Activate()
    {
        DecreaseDefence def = new();
        def.SetDebuff(1, target, 10);
        return true;
    }
}
