using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepenceUp : Active
{
    public override bool Activate()
    {
        IncreaseDefence buff = new();
        buff.SetBuff(1, target, 10);
        return true;
    }
}
