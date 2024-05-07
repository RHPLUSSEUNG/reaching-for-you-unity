using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Debuff
{
    short decrease;
    public override void SetDebuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        remainTurn = turn;
        decrease = attribute;
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        target.GetComponent<CharacterSpec>().shield -= decrease;
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterSpec>().shield += decrease;
            DeleteEffect();
        }

    }
}
