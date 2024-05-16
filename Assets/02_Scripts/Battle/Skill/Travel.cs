using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Travel : Skill
{
    public abstract bool Activate(); //use skill
    public override void Start()
    {
        Managers.Data.SetSkill(skillId, this);
    }
}
