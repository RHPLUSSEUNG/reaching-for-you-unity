using UnityEngine;

public class Queen_AtkUp : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // �Ʊ� ���ݷ� ����
        Managers.Active.ModifyAtk(target, 20);
        return true;
    }
}