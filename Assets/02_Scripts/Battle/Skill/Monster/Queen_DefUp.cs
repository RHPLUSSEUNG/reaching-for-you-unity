using UnityEngine;

public class Queen_DefUp : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // 아군 방어력 버프
        Managers.Active.ModifyDefense(target, 20);
        return true;
    }
}