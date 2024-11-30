using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Staff : Equipment
{
    public override bool Equip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, 20);
        character.GetComponent<CharacterState>().closeAttack = false;
        character.GetComponent <CharacterState>().AttackType = ElementType.Fire;
        character.GetComponent<EntityStat>().AttackRange = 4;
        return true;
    }

    public override bool UnEquip(GameObject character)
    {
        if (character == null) return false;
        Managers.Active.ModifyAtk(character, -20);
        return true;
    }
}
