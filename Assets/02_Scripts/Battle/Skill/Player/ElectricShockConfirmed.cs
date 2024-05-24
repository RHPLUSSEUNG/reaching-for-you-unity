using UnityEngine;

public class ElectricShockConfirmed : Active
{
    public override bool Activate()
    {
        //TODO Effect
        Debug.Log("Electric Shock");
        ElectricShock shock = new();
        shock.SetDebuff(1, target, 1);
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        //TODO 범위 확인 -> false
        if (target.GetComponent<EntityStat>() == null)
        {
            return false;
        }
        this.target = target;
        Activate();
        return true;
    }
}