using UnityEngine;

public class Defense_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;

        IncreaseDefense buff = new();
        buff.SetBuff(3, target, 5);
        return true;
    }
}