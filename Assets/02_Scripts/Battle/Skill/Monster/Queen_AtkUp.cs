using UnityEngine;

public class Queen_AtkUp : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // 아군 공격력 버프
        Managers.Active.ModifyAtk(target, 20);
        return true;
    }
}