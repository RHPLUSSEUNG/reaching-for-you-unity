using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : Active
{
    public override bool Activate()
    {
        CharacterState state = target.GetComponent<CharacterState>();
        if(state.debuffs.Count > 0)
        {
            state.DelDebuff(state.debuffs[0]);
            return true;
        }
        return false;
    }
}
