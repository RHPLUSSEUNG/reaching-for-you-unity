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
            target.GetComponent<SkillList>().buffs.Remove(this);
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        this.remainTurn = turn;
        incAtk = attribute;
        target.GetComponent<SkillList>().buffs.Add(this);
    }

    public override bool StartEffect()
    {
        if (this.target == null)
            return false;
        this.target.GetComponent<CharacterSpec>().attack += incAtk;
        TimeCheck();
        return true;
    }
}
