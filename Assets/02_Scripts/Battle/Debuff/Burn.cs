using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Debuff
{
    private short tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        target.GetComponent<CharacterBattle>().GetDamaged(tickDmg, ElementType.Fire);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(short turn, GameObject target, short attribute)
    {
        this.target = target;
        remainTurn = turn;
        tickDmg = attribute;
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        return true;
    }
}
