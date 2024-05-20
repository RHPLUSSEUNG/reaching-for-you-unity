using UnityEngine;

public class MP_Portion_Mid : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<EntityStat>().Mp += 50;
        return true;
    }
}