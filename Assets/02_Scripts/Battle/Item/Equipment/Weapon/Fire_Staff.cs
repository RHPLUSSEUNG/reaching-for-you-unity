using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Staff : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        return true;
    }
}
