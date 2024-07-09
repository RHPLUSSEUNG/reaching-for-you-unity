using UnityEngine;

public class MP_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        Managers.Active.MPRecovery(target, 100);
        return true;
    }
}
