using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEveryTurn : Buff
{
    int healing;
    public override void TimeCheck()
    {
        remainTurn--;
        Managers.Active.Heal(target, healing);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }
    public override bool StartEffect()
    {
        return true;
    }

    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        this.healing = attribute;
        AddBuff(target, TurnEnd);
        MakeEffectAnim();
        this.StartEffect();
    }
}
