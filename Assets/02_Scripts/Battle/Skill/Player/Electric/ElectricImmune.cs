using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricImmune : Passive
{
    public override bool Activate(GameObject character)
    {
        character.GetComponent<CharacterState>().can_immune = true;
        return false;
    }

    public override bool UnActivate(GameObject character)
    {
        character.GetComponent<CharacterState>().can_immune = true;
        return true;
    }
}
