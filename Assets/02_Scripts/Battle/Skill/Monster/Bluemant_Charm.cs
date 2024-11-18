using UnityEngine;

public class Bluemant_Charm : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyAtk(target, 30);
        // 공격력 감소
        return true;
    }
}