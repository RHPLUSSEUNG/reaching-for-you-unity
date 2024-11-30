using UnityEngine;

public class BarrierBuff : Buff
{
    int attribute;
    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.remainTurn = turn;
        this.target = target;
        this.attribute = attribute;
        AddBuff(target, TurnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        target.GetComponent<CharacterState>().AddBarrier(attribute);
        TimeCheck();
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if(remainTurn == 0)
        {
            target.GetComponent<CharacterState>().DeleteBarrier(attribute);
            DeleteEffect();
        }
    }
}
