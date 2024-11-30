using UnityEngine;

public class Soldier_Sting : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        Managers.Active.Damage(target, 60);
        return true;
    }
}
