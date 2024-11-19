using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Hammer : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, 30);
        character.GetComponent<CharacterState>().closeAttack = true;
        character.GetComponent<CharacterState>().AttackType = ElementType.Ground;
        character.GetComponent<CharacterState>().knock_back = true;
        character.GetComponent<EntityStat>().AttackRange = 1;
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.sprite;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, -30);
        character.GetComponent<CharacterState>().knock_back = false;
        character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        return true;
    }
}
