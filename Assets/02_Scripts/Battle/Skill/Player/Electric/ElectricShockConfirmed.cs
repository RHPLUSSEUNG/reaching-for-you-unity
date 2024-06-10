using UnityEngine;

public class ElectricShockConfirmed : Active
{
    public override bool Activate()
    {
        //TODO Effect
        ElectricShock shock = new();
        shock.SetDebuff(2, target, 1);
        return true;
    }
}