using UnityEngine;

public class Crab_Skill : Active
{
    public override bool Activate()
    {
        GameObject current = Managers.Battle.currentCharacter;

        // È¸º¹·®
        Managers.Active.Heal(target,100);

        return true;
    }
}
