using UnityEngine;

public class Burn : Debuff
{
    public int tickDmg = 10;
    public int stack;
    public override void TimeCheck()
    {
        remainTurn--;
        if (target.GetComponent<CharacterState>().GetRecoveryFire())
        {
            Managers.Active.Heal(target, tickDmg);
        }
        else
        {
            Managers.Active.Damage(target, tickDmg*stack, ElementType.Fire);
        }
        
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute, bool turnEnd = false)
    {
        CharacterState status = target.GetComponent<CharacterState>();
        Burn pos = (Burn)status.FindDebuff(this);
        if (pos != null)
        {
            pos.remainTurn += remainTurn;
            pos.stack += stack;
            return;
        }
        this.target = target;
        remainTurn = turn;
        stack = attribute;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
        MakeEffectAnim();
        StartEffect();
    }

    public override bool StartEffect()
    {
        if (target == null)
            return false;
        
        return true;
    }
}
