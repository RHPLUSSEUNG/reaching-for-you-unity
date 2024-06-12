using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDebuff : Debuff
{
    int damage;

    public override void SetDebuff(int turn, GameObject target, short attribute = 0, bool TurnEnd = false)
    {
        this.target = target;
        this.remainTurn = turn;
        this.damage = attribute;
        target.GetComponent<CharacterState>().AddDebuff(this, TurnEnd);
        StartEffect();
    }

    public override bool StartEffect()
    {
        target.GetComponent<CharacterState>().ChangeStun(true);
        return true;
    }

    public override void TimeCheck()
    {
        remainTurn--;
        if (remainTurn == 0)
        {
            if(target.GetComponent<EntityStat>().Hp > 0)
            {
                Managers.Active.Damage(target, damage, ElementType.Water);
            }
            target.GetComponent<CharacterState>().ChangeStun(false);
            DeleteEffect();
        }
    }

}
