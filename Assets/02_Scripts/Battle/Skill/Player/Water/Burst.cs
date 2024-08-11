using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : Active
{
    public override bool Activate()
    {
        IncreaseAtk buff = new();
        buff.SetBuff(1, target, 20);
        return true;
    }
}
