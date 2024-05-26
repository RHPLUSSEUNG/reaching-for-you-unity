using UnityEngine;

public class Burn : Debuff
{
    public int tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        Managers.Active.Damage(target, tickDmg);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute)
    {
        this.target = target;
        remainTurn = turn;
        tickDmg = attribute;
        AddDeBuff(target);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        
        return true;
    }

    public override void Duplicate_Debuff(Debuff debuff)
    {
        Burn burn = (Burn)debuff;
        this.remainTurn = debuff.remainTurn;
        this.tickDmg += burn.tickDmg;
        this.count++;
    }
}
