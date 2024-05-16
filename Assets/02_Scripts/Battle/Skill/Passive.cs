using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : Skill
{
    public abstract bool Activate(); //equip passive
    public abstract bool UnActivate(); //unequip passive
    public override void Start()
    {
        Managers.Data.SetSkill(skillId, this);
    }
}
