using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanElectricShock : Active
{
    public override bool Activate()
    {
        CharacterState targetState = Managers.Battle.currentCharacter.GetComponent<CharacterState>();
        ElectricShock shock = new();
        if(targetState.FindDebuff(shock) != null)
        {
            targetState.FindDebuff(shock).count--;
            return true;
        }
        return false;
    }
}
