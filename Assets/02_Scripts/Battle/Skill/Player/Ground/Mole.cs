using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Active
{
    public override bool Activate()
    {
        Hide hide = new();
        hide.SetBuff(1, target);
        return true;
    }
}
