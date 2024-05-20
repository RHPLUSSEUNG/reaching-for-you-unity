using UnityEngine;

public class Speed_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        IncreaseSpeed buff = new();
        buff.SetBuff(5, target, 3);
        return true;
    }
}