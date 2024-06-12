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
        this.StartEffect();
    }

    public override bool StartEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void TimeCheck()
    {
        throw new System.NotImplementedException();
    }
}
