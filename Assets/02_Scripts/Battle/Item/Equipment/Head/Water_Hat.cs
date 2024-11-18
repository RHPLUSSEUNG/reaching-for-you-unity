using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Hat : Equipment
{
    public override bool Equip(GameObject character)
    {
        Managers.Active.ModifyDefense(character, 10);
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        Managers.Active.ModifyDefense(character, -10);
        return true;
    }
}
