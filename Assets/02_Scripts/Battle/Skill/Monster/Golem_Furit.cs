using UnityEngine;

public class Golem_Furit : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // ȸ����
        Managers.Active.Heal(target, 100);

        return true;
    }
}