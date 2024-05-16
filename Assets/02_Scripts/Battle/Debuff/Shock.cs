using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Debuff
{
    int stack;
    public override void SetDebuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        remainTurn = turn;
    }

    public override bool StartEffect()
    {
        if(target == null)
            return false;
        target.GetComponent<CharacterSpec>().remainStamina = 0;
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }
}
