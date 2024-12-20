using UnityEngine;

public class IncreaseSpeed : Buff
{
    private int incMove;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifySpeed(target, -1 * incMove);
            DeleteEffect();
        }
    }

    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        incMove = attribute;
        AddBuff(target, TurnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        Managers.Active.ModifySpeed(target, incMove);
        TimeCheck();
        return true;
    }
}
