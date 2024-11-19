using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Hat : Equipment
{
    public override bool Equip(GameObject character)
    {
        Managers.Active.ModifyDefense(character, 10);
        character.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = this.sprite;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        Managers.Active.ModifyDefense(character, -10);
        return true;
    }
}
