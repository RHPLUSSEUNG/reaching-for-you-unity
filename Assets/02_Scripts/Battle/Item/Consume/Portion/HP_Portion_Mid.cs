using UnityEngine;

public class HP_Portion_Mid : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        Managers.Active.Heal(target, 75);
        return true;
    }
}