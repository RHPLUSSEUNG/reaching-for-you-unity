using UnityEngine;

public class HP_Portion_Big : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        Managers.Active.Heal(target, 100);
        return true;
    }
}
