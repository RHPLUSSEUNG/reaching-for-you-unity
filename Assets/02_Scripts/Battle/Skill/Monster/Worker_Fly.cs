using UnityEngine;

public class Worker_Fly : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // ȸ���� ���
        return true;
    }
}