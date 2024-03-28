using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Debuff
{
    private short tickDmg;
    public override void Effect()
    {
        remainTurn--;
        this.gameObject.GetComponent<CharacterBattle>().GetDamaged(remainTurn, ElementType.Fire);
        if(remainTurn == 0)
        {
            DeleteEffect();
        }
    }

    public override void SetDebuff(short turn, GameObject target, short tickDMG)
    {
        remainTurn = turn;
        tickDmg = tickDMG;
        target.AddComponent<Burn>();
    }
}
