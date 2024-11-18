using UnityEngine;

public class Mermaid_Sing : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyDefense(target, 20);
        // 공격력 감소, 익사 부여
        return true;
    }
}