using UnityEngine;

public class Mermaid_Sing : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.ModifyDefense(target, 20);
        BubbleDebuff debuff = new BubbleDebuff();
        debuff.SetDebuff(2, target);
        // ���ݷ� ����, �ͻ� ����� 2 �ο�
        return true;
    }
}