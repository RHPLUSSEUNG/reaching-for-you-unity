using UnityEngine;

public class Sh_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        IncreaseShield buff = new IncreaseShield();
        buff.SetBuff(5, target, 10);
        buff.Active();
        return true;
    }
}
