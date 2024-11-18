using UnityEngine;

public class Mermaid_Sing : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyDefense(target, 20);
        BubbleDebuff debuff = new BubbleDebuff();
        debuff.SetDebuff(2, target);
        // 공격력 감소, 익사 디버프 2 부여
        return true;
    }
}