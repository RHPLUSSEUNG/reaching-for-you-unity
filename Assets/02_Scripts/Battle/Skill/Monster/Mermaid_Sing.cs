using UnityEngine;

public class Mermaid_Sing : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyDefense(target, 20);
        // ���ݷ� ����, �ͻ� �ο�
        return true;
    }
}