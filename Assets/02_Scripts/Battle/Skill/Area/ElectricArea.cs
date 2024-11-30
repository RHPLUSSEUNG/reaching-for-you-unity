using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArea : AreaInterface
{
    public override void CalcTurn()
    {
        GetCharacter();
        foreach (Collider collider in colliders)
        {
            ElectricShock shock = new();
            if(collider.gameObject == Managers.Battle.currentCharacter)
            {
                shock.SetDebuff(3, collider.gameObject, 0, true);
            }
        }
        DestroyArea();
    }
}
