using UnityEngine;

public class Bluemant_Charm : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyAtk(target, 30);
        Cold debuff = new Cold();
        debuff.SetDebuff(3, target);
        // ���ݷ� ����, ���� ����� 3 �ο�
        return true;
    }
}