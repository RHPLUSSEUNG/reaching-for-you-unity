using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityRecovery : Active
{
    public override bool Activate()
    {
        Managers.Active.Heal(target, Managers.Battle.currentCharacter.GetComponent<CharacterState>().capacityStack * 5 * 10);
        Managers.Battle.currentCharacter.GetComponent<CharacterState>().capacityStack = 0;
        return true;
    }
}
