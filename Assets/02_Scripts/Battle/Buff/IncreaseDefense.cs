using UnityEngine;

public class IncreaseDefense : Buff
{
    private short incShd;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifyDefense(target, -1 * incShd);
            DeleteEffect();
        }
    }

    public override void SetBuff(short turn, GameObject target, short attribute = 0)
    {
        this.target = target;
        this.remainTurn = turn;
        incShd = attribute;
        AddBuff(target);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        Managers.Active.ModifyDefense(target, incShd);
        TimeCheck();
        return true;
    }
}
