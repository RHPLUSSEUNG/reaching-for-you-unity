using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAtk : Buff
{
    private short incAtk;
    public override void TimeCheck()
    {
        remainTurn--;
        if(remainTurn ==  0)
        {
            this.gameObject.GetComponent<CharacterSpec>().attack -= incAtk;
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.remainTurn = turn;
        incAtk = attribute;
        target.GetComponent<PlayerSpec>().buffs.Add(this);
    }

    public override bool StartEffect()
    {
        if (this.gameObject == null)
            return false;
        this.gameObject.GetComponent<CharacterSpec>().attack += incAtk;
        TimeCheck();
        return true;
    }
}
