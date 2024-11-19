using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Staff : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, 20);
        character.GetComponent<CharacterState>().closeAttack = false;
        character.GetComponent<CharacterState>().AttackType = ElementType.Grass;
        character.GetComponent<EntityStat>().AttackRange = 4;
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.sprite;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, -20);
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        return true;
    }
}
