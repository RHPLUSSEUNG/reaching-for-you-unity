using UnityEngine;

public class MP_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        Managers.Active.MPRecovery(target, 50);
        return true;
    }
}