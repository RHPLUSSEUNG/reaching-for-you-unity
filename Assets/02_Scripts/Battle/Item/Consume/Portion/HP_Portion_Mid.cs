using UnityEngine;

public class HP_Portion_Mid : Consume
{
    public override bool Activate(GameObject target)
    {
        if(target == null) return false;
        target.GetComponent<EntityStat>().Hp += 50;
        return true;
    }
}