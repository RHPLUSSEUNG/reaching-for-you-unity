using UnityEngine;

public class ParalysisProbailityUp : Active
{
    public override bool Activate()
    {
        //TODO Effect
        ElectricShock shock = new();
        shock.SetDebuff(5, target, 5);
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