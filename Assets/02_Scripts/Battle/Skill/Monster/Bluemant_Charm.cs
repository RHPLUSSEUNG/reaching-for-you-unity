using UnityEngine;

public class Bluemant_Charm : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyAtk(target, 30);
        Cold debuff = new Cold();
        debuff.SetDebuff(3, target);
        // 공격력 감소, 추위 디버프 3 부여
        return true;
    }
}