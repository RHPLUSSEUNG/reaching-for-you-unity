using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Freeze : Debuff
{
    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)
    {
        CharacterState status = target.GetComponent<CharacterState>();
        Freeze pos = (Freeze)status.FindDebuff(this);
        if (pos != null)
        {
            pos.remainTurn += remainTurn;
            return;
        }
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
            target.GetComponent<CharacterState>().ChangeFreeze(false);
            DeleteEffect();
        }
    }
}
