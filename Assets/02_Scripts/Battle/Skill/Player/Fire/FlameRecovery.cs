using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameRecovery : Active
{
    public override bool Activate()
    {
        BurnRecoveryBuff buff = new BurnRecoveryBuff();
        buff.SetBuff(2, target);
        return true;
    }

}
