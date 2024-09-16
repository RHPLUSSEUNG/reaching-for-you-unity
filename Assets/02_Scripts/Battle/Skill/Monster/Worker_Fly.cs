using UnityEngine;

public class Worker_Fly : MonsterSkill
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // È¸ÇÇÀ² »ó½Â
        return true;
    }
}