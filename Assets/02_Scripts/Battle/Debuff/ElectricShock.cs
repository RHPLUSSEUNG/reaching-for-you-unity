using UnityEngine;

public class ElectricShock : Debuff
{
    public override void TimeCheck()
    {
        remainTurn--;
        if(count*5 >= Random.Range(1, 101))
        {
            Paralysis paralysis = new();
            paralysis.SetDebuff(1, target,0,true);
            DeleteEffect();
        }
        if(remainTurn <= 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(int turn, GameObject target, short attribute, bool turnEnd = false)
    {
        this.target = target;
        remainTurn = turn;
        count = attribute;
        target.GetComponent<CharacterState>().AddDebuff(this, turnEnd);
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
        ElectricShock shock = (ElectricShock)debuff;
        count += shock.count;
    }
}
