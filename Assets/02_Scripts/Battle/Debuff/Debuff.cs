using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : MonoBehaviour
{
    public short remainTurn;

    public abstract void Effect();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }

        Destroy(this.gameObject.GetComponent<Debuff>());
        return true;
    }

    public abstract void SetDebuff(short turn, GameObject target, short TickDMG = 0);
    
    public void Active()
    {
        Effect();
    }
}