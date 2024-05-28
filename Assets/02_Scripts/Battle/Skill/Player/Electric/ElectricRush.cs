using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricRush : Active
{
    Vector3 rotation;
    int mask = 1 << 2;
    public override bool Activate()
    {
        Managers.Battle.currentCharacter.transform.position += rotation * 4;
        Managers.Active.Damage(target, target.GetComponent<EntityStat>().Hp / 4, ElementType.Electric);
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        Debug.Log(mask);
        this.target = target;
        if (target.GetComponent<EntityStat>() == null)
        {
            return false;
        }
        if (CalcRange())
        {
            return Activate();
        }
        return false;
    }

    private bool CalcRange()
    {
        this.transform.position = target.transform.position;
        rotation = (target.transform.position-Managers.Battle.currentCharacter.transform.position).normalized;
        Collider[] colliders = Physics.OverlapBox(this.transform.position + rotation*2 , rotation*2, Managers.Battle.currentCharacter.transform.rotation);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject !=  this.target && collider.gameObject.layer != mask)
            {
                Debug.Log(collider.gameObject);
            }
        }
        return true;
    }
}
