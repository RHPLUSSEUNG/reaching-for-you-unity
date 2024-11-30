using UnityEngine;

public class IncreaseAtk : Buff
{
    private int incAtk;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifyAtk(target, -1 * incAtk);
            DeleteEffect();
        }
    }

    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        incAtk = attribute;
        AddBuff(target, TurnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (this.target == null)
            return false;
        Managers.Active.ModifyAtk(target, incAtk);
        TimeCheck();
        return true;
    }
}
