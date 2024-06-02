using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuff : Buff
{
    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        AddBuff(target, TurnEnd);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (this.target == null)
            return false;
        target.GetComponent<CharacterState>().ChangeGhost(true);
        TimeCheck();
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            target.GetComponent<CharacterState>().ChangeGhost(false);
            DeleteEffect();
        }
    }
}
