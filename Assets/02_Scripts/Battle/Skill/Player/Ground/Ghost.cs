using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Active
{
    public override bool Activate()
    {
        GhostBuff buff = new();
        buff.SetBuff(5, target);
        return true;
    }
}
