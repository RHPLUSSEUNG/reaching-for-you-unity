using UnityEngine;

public class Queen_DefUp : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // �Ʊ� ���� ����
        Managers.Active.ModifyDefense(target, 20);
        return true;
    }
}