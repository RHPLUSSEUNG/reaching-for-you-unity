using UnityEngine;

public class IncreaseSpeed : Buff
{
    private short incMove;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifySpeed(target, -1 * incMove);
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        this.remainTurn = turn;
        incMove = attribute;
        AddBuff(target);
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
