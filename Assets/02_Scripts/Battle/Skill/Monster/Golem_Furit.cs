using UnityEngine;

public class Golem_Furit : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // È¸º¹·®
        Managers.Active.Heal(target, 100);

        return true;
    }
}