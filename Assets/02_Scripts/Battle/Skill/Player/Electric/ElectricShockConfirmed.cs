using UnityEngine;

public class ElectricShockConfirmed : Active
{
    public override bool Activate()
    {
        //TODO Effect
        ElectricShock shock = new();
        shock.SetDebuff(1, target, 1);
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        if (target.GetComponent<EntityStat>() == null)
        {
            return false;
        }
        this.target = target;
        if (!Activate())
        {
            return false;
        }
        return true;
    }
}