using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverFlame : Active
{
    public override bool Activate()
    {
        SkillExtent extent = Managers.Skill.extent.GetComponent<SkillExtent>();
        List<GameObject> opponents = extent.SetArea(2, TargetObject.Enemy, target.transform.position, true);
        if (opponents.Count <= 0)
        {
            return true;
        }
        foreach (GameObject opponent in opponents)
        {
            if (opponent != null || opponent.GetComponent<CharacterState>()!= null)
            {
                Managers.Active.Damage(opponent, 35, element);
                Burn burn = new Burn();
                burn.SetDebuff(2, opponent, 10);
            }
        }
        return true;
    }

}
