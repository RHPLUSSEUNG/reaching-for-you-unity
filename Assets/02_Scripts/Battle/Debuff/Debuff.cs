using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    public GameObject target;
    public short remainTurn;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }

        target.GetComponent<CharacterSpec>().debuffs.Remove(this);
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetDebuff(short turn, GameObject target, short attribute = 0);

    public void Active()
    {
        StartEffect();
    }

}
