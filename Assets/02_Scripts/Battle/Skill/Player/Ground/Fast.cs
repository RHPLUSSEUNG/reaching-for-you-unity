using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast : Active
{
    public override bool Activate()
    {
        IncreaseSpeed fast = new();
        fast.SetBuff(1, target, 5);
        return true;
    }
}
