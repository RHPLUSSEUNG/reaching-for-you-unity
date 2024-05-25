using UnityEngine;

public class ElectricShockArea : Active
{
    public override bool Activate()
    {
        //TODO Effect
        GameObject area = Managers.Prefab.Instantiate("Area/ElectricArea");
        area.transform.position = target.transform.position;
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