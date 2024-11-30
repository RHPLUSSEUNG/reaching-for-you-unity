using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighRecovery : Active
{
    public override bool Activate()
    {
        HealEveryTurn recovery = new();
        recovery.SetBuff(4, target, 40);
        return true;
    }
}