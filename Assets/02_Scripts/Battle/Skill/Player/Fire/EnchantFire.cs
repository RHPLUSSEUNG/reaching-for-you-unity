using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantFire : Active
{
    public override bool Activate()
    {
        FireEnchant enchant = new ();
        enchant.SetBuff(1, target);
        return true;
    }
}