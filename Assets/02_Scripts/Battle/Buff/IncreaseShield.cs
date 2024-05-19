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
            target.GetComponent<CharacterSpec>().attack -= incShd;
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.remainTurn = turn;
        incShd = attribute;
        target.GetComponent<CharacterSpec>().buffs.Add(this);
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<CharacterSpec>().attack += incShd;
        TimeCheck();
        return true;
    }
}
