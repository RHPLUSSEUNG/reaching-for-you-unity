using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric_Shock : Active
{
    GameObject target;
    public override bool Activate()
    {
        //TODO Effect
        Debug.Log("Electric Shock");
        
        Shock shock = new Shock();
        shock.SetDebuff(1, target);
        shock.StartEffect();
        target.GetComponent<CharacterSpec>().debuffs.Add(shock);
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        //TODO 범위 확인 -> false
        if (target.GetComponent<CharacterSpec>() == null)
        {
            return false;
        }
        this.target = target;
        Activate();
        return true;
    }

    public override void setSkill()
    {
        skillName = "Electric_Shock";
        this.type = skillType.Active;
        this.element = ElementType.Electric;
    }
}