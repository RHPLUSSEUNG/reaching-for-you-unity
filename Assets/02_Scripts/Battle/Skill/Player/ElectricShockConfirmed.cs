using UnityEngine;

public class ElectricShockConfirmed : Active
{
    GameObject target;
    public override bool Activate()
    {
        //TODO Effect
        Debug.Log("Electric Shock");
        /*
        Shock shock = new();
        shock.SetDebuff(1, target);
        shock.StartEffect();
        target.GetComponent<CharacterSpec>().debuffs.Add(shock);
        */
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