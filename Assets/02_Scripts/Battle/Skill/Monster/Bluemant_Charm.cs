using UnityEngine;

public class Bluemant_Charm : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyAtk(target, 30);
        // ���ݷ� ����
        return true;
    }
}