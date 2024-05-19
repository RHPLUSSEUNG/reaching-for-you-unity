using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShield : Buff
{
    private short incShd;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<EntityStat>().BaseDamage -= incShd;
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.remainTurn = turn;
        incShd = attribute;
        target.GetComponent<SkillList>().buffs.Add(this);
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<EntityStat>().BaseDamage += incShd;
        TimeCheck();
        return true;
    }
}
