using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSkill : Active
{
    public override bool Activate()
    {
        Slow slow = new();
        slow.SetDebuff(1, target, 5);
        target.GetComponent<CharacterState>().AddDebuff(slow);
        return true;
    }
}
