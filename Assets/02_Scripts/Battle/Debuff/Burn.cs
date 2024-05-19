using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Debuff
{
    public int tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        Managers.Party.Damage(target, tickDmg);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute)
    {
        this.target = target;
        remainTurn = turn;
        tickDmg = attribute;

        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        
        return true;
    }

    public override void AddDebuff(Debuff debuff)
    {
        Burn burn = (Burn)debuff;
        this.remainTurn = debuff.remainTurn;
        this.tickDmg += burn.tickDmg;
        this.count++;
    }
}
