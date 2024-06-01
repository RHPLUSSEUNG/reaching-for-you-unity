using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDMG : Active
{
    public override bool Activate()
    {
        int stack = Managers.Battle.currentCharacter.GetComponent<CharacterState>().capacityStack;
        if (stack == 0) return false;

        IncreaseAtk buff = new();
        buff.SetBuff(stack, target, stack * 5);
        return true;
    }
}