using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Active
{
    public override bool Activate()
    {
        HealEveryTurn recovery = new();
        recovery.SetBuff(4, target, 20);
        return true;
    }
}