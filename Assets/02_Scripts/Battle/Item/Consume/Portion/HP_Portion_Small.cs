using UnityEngine;

public class HP_Portion_Small : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<EntityStat>().Hp += 75;
        return true;
    }
}