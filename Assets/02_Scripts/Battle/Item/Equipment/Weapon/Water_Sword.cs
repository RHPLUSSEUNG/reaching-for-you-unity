using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Sword : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, 20);
        character.GetComponent<CharacterState>().closeAttack = true;
        character.GetComponent<CharacterState>().AttackType = ElementType.Water;
        character.GetComponent<EntityStat>().AttackRange = 1;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, -20);
        return true;
    }
}
