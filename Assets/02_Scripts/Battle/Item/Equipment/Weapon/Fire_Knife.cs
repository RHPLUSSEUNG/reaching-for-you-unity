using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Knife : Equipment
{
	public override bool Equip(GameObject character)
	{
		if (character == null) return false;
		Managers.Active.ModifyAtk(character, 15);
		character.GetComponent<CharacterState>().closeAttack = true;
		character.GetComponent<CharacterState>().AttackType = ElementType.Fire;
        character.GetComponent<CharacterState>().after_move = true;
        character.GetComponent<EntityStat>().AttackRange = 2;
		Managers.Active.ModifySpeed(character, 10);
		return true;
	}

	public override bool UnEquip(GameObject character)
	{
		if (character == null) return false;
		Managers.Active.ModifyAtk(character, -15);
        character.GetComponent<CharacterState>().after_move = false;
        return true;
	}
}
