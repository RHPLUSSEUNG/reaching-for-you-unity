using UnityEngine;

public class Crab_Skill: MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // È¸º¹·®
        Managers.Active.Heal(target,20);

        return true;
    }
}
