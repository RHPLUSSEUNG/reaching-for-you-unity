using UnityEngine;

public class Poision : Debuff
{
    int tickDmg =10;
    string debuffname = "Poision";
    public override void TimeCheck()
    {
        remainTurn--;
        Managers.Active.Damage(target, tickDmg, ElementType.Grass);
        tickDmg = tickDmg/10*2;
        if (remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool turnEnd = false)
    {
        CharacterState status = target.GetComponent<CharacterState>();
        Debuff pos = status.FindDebuff(this);
        if(pos != null)
        {
            pos.remainTurn += remainTurn;
            return;
        }
        this.target = target;
        tickDmg = attribute;
        remainTurn = turn;
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
