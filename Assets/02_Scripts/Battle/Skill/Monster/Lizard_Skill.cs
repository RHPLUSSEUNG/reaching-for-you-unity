using UnityEngine;

public class Lizard_Skill : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // ����
        Managers.Active.Damage(target, 30, ElementType.None, true);

        return true;
    }
}
