using UnityEngine;

public abstract class Passive : Skill
{
    public abstract bool Activate(GameObject character); //equip passive
    public abstract bool UnActivate(GameObject character); //unequip passive
    public override void Start()
    {
        Managers.Data.SetPlayerSkill(skillId, this);
    }
}