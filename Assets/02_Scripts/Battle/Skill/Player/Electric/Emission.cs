using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : Active
{
    public override bool Activate()
    {
        SkillExtent extent = Managers.Skill.extent.GetComponent<SkillExtent>();
        List<GameObject> targets = extent.SetArea(3, TargetObject.Enemy, target.transform.position, false);
        int damage = Managers.Battle.currentCharacter.GetComponent<CharacterState>().capacityStack * 5 * Managers.Battle.currentCharacter.GetComponent<EntityStat>().BaseDamage;
        if(targets.Count <= 0)
        {
            return true;
        }
        foreach(GameObject target in targets)
        {
            Debug.Log(target);
            Managers.Active.Damage(target, damage, ElementType.Electric);
        }
        return true;
    }
    public override bool SetTarget(GameObject target)
    {
        if (target.GetComponent<EntityStat>() == null)
        {
            return false;
        }
        this.target = target;
        if (!Activate())
        {
            return false;
        }
        return true;
    }
}
