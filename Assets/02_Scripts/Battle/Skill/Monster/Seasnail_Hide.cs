using UnityEngine;

public class Seasnail_Hide : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyDefense(target, 40);
        // �� ���
        return true;
    }
}