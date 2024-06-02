using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capcity_Me : Passive
{
    public override bool Activate(GameObject character)
    {
        if (character == null)
            return false;
        character.GetComponent<CharacterState>().capacity += 1;
        return true;
    }

    public override bool UnActivate(GameObject character)
    {
        if (character == null)
            return false;
        character.GetComponent<CharacterState>().capacity -= 1;
        return true;
    }
}
