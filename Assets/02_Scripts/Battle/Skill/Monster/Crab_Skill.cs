using UnityEngine;

public class Crab_Skill : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // ȸ����
        Managers.Active.Heal(target,100);

        return true;
    }
}
