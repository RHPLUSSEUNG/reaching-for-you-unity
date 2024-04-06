using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Debuff
{
    private short tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        this.gameObject.GetComponent<CharacterBattle>().GetDamaged(remainTurn, ElementType.Fire);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(short turn, GameObject target, short attribute)
    {
        remainTurn = turn;
        tickDmg = attribute;
    }

    public override bool StartEffect()
    {
        if (this.gameObject == null)
            return false;
        TimeCheck();
        return true;
    }
}
