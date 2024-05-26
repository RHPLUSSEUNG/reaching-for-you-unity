using UnityEngine;

public class MP_Portion_Mid : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        Managers.Active.MPRecovery(target, 75);
        return true;
    }
}