using UnityEngine;

public class IncreaseDefence : Buff
{
    private int incShd;
    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            Managers.Active.ModifyDefense(target, -1 * incShd);
            DeleteEffect();
        }
    }

    public override void SetBuff(int turn, GameObject target, int attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        incShd = attribute;
        AddBuff(target, TurnEnd);
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
