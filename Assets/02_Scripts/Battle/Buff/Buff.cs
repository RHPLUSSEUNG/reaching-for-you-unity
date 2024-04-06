using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public short remainTurn;

    public abstract void TimeCheck();

    public bool DeleteEffect()
    {
        if (remainTurn > 0)
        {
            return false;
        }

        Destroy(this.gameObject.GetComponent<Buff>());
        return true;
    }

    public abstract bool StartEffect();

    public abstract void SetBuff(short turn, GameObject target, short attribute = 0);

    public void Active()
    {
        StartEffect();
    }

}
