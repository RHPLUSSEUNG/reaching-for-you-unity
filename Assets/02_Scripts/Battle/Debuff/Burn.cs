using UnityEngine;

public class Burn : Debuff
{
    public int tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        if (target.GetComponent<CharacterState>().GetRecoveryFire())
        {
            Managers.Active.Heal(target, tickDmg);
        }
        else
        {
            Managers.Active.Damage(target, tickDmg, ElementType.Fire);
        }
        
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute, bool turnEnd = false)
    {
        this.target = target;
        remainTurn = turn;
        tickDmg = attribute;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        
        return true;
    }
}
