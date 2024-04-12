using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdose : Debuff
{
    private short tickDmg;
    public override void SetDebuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        remainTurn = turn;
        tickDmg = attribute;
    }

    public override bool StartEffect()
    {
        if(target == null)
            return false;
        TimeCheck();
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        target.GetComponent<CharacterBattle>().GetDamaged(tickDmg, ElementType.Grass);
        tickDmg = (short)(tickDmg / 2 * 3);
        if(remainTurn == 0 )
            DeleteEffect();
    }

}
