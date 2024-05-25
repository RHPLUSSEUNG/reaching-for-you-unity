using UnityEngine;

public class IncreaseAtk : Buff
{
    private short incAtk;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifyAtk(target, -1 * incAtk);
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        this.remainTurn = turn;
        incAtk = attribute;
        AddBuff(target);
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
