using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Debuff
{
    short stack;
    public override void SetDebuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        remainTurn = turn;
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
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public void IncreaseStack()
    {
        stack++;
        if(stack== 2)
        {
            target.GetComponent<CharacterSpec>().remainStamina = 0;
        }
    }
}
