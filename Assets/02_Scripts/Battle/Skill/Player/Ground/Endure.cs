using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Endure : Active
{
    public override bool Activate()
    {
        endureBuff endure = new();
        endure.SetBuff(1,target);

        IncreaseDefence def = new();
        def.SetBuff(1, target, 15);

        return true;
    }
}
