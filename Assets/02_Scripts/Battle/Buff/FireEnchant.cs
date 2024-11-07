using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnchant : Buff
{
    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        remainTurn = turn;
        this.target = target;
    }

    public override bool StartEffect()
    {
        target.GetComponent<CharacterState>().ChangeEnchantFire(1);
        MakeEffectAnim();
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeEnchantFire(-1);
            DeleteEffect();
        }
    }
}
