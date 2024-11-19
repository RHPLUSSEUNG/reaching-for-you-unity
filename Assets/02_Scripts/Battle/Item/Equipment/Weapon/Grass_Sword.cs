using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Sword : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, 30);
        character.GetComponent<CharacterState>().closeAttack = true;
        character.GetComponent<CharacterState>().AttackType = ElementType.Grass;
        character.GetComponent<EntityStat>().AttackRange = 1;

        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.sprite;
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, -30);
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        return true;
    }
}
