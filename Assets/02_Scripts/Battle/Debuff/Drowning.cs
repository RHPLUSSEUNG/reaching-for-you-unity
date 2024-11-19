using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drowning : Debuff
{
    int stack;
    int damage = 10;

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)//turn = int.maxValue() attribute = startStack
    {
        CharacterState status = target.GetComponent<CharacterState>();
        Drowning pos = (Drowning)status.FindDebuff(this);
        if (pos != null)
        {
            pos.stack += attribute;
            return;
        }
        this.target = target;
        this.remainTurn = int.MaxValue;
        this.stack = attribute;
        target.GetComponent<CharacterState>().AddDebuff(this, TurnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        stack--;
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        stack--;
        if (stack <= 0)
        {
            Managers.Active.Damage(target, damage);
        }
        if (remainTurn == 0)
        {
            DeleteEffect();
        }
    }

}
