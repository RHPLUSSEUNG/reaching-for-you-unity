using UnityEngine;

public class Poision : Debuff
{
    int tickDmg;
    public override void TimeCheck()
    {
        remainTurn--;
        Managers.Active.Damage(target, tickDmg, ElementType.Grass);
        tickDmg = tickDmg/2*3;
        if (remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool turnEnd = false)
    {
        this.target = target;
        tickDmg = attribute;
        remainTurn = turn;
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
