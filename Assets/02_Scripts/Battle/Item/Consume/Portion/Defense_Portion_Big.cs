using UnityEngine;

public class Defense_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        IncreaseDefence buff = new ();
        buff.SetBuff(5, target, 10);
        return true;
    }
}
