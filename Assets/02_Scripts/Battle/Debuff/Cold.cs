using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold : Debuff
{
    int stack;
    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)
    {
        this.remainTurn = turn;
        this.target = target;
        this.stack = attribute;
        StartEffect();
        target.GetComponent<CharacterState>().AddDebuff(this);
    }

    public override bool StartEffect()
    {
        target.GetComponent<CharacterState>().ChangeCold(stack);
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if (target.GetComponent<CharacterState>().GetCold() > 5)
        {
            Freeze freeze = new();
            freeze.SetDebuff(1, target, 0, true);
        }
        if (remainTurn <= 0)
        {
            target.GetComponent<CharacterState>().ChangeCold(-stack);
            DeleteEffect();
        }
    }
}
