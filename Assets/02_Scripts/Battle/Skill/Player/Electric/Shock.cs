using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Passive
{
    public override bool Activate(GameObject character)
    {
        character.GetComponent<CharacterState>().can_shock = true;
        return true;
    }

    public override bool UnActivate(GameObject character)
    {
        character.GetComponent<CharacterState>().can_shock = false;
        return false;
    }
}
