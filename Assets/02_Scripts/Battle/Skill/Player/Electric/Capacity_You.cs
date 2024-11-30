using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Capacity_You : Passive
{
    public override bool Activate(GameObject character)
    {
        if (character == null)
            return false;
        character.GetComponent<CharacterState>().capacity += 2;
        return true;
    }

    public override bool UnActivate(GameObject character)
    {
        if (character == null)
            return false;
        character.GetComponent<CharacterState>().capacity -= 2;
        return true;
    }
}
