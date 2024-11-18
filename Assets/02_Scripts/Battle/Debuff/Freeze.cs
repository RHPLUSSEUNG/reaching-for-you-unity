using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Freeze : Debuff
{
    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)
    {
        this.remainTurn = turn;
        this.target = target;
        target.GetComponent<CharacterState>().AddDebuff(this);
        MakeEffectAnim();
        this.StartEffect();
    }

    public override bool StartEffect()
    {
        target.GetComponent<CharacterState>().ChangeFreeze(true);
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        
        if(remainTurn <= 0)
        {
            DeleteEffect();
        }
    }
}
