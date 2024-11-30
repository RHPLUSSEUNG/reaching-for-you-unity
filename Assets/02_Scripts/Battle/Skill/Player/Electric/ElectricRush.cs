using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricRush : Active
{
    Vector3 rotation;
    Collider[] colliders;
    int mask = 1 << 3;
    public override bool Activate()
    {
        rotation.y = 0;
        Managers.Battle.currentCharacter.transform.position += rotation * 4;
        for(int i = 0; i < colliders.Length; i++)
        {
            GameObject target = colliders[i].gameObject.transform.parent.gameObject;
            Debug.Log(target);
            Managers.Active.Damage(target, target.GetComponent<EntityStat>().Hp / 4, ElementType.Electric, true);
        }
        
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        this.target = target;
        if (target.GetComponent<EntityStat>() == null)
        {
            return false;
        }
        if (!CalcRange())
        {
            return false;
        }
        if (Activate())
        {
            Managers.Manager.StartCoroutine(Managers.Skill.StartEffect(Effect, target.transform.position, other));
            return true;
        }
        return false;
    }

    private bool CalcRange()
    {
        this.transform.position = target.transform.position;
        rotation = (target.transform.position-Managers.Battle.currentCharacter.transform.position).normalized;
        colliders = Physics.OverlapBox(this.transform.position + rotation*2 , rotation*2, Managers.Battle.currentCharacter.transform.rotation);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.transform.parent.gameObject);
            if(collider.gameObject.layer == mask)
            {
                Debug.Log(mask);
                Debug.Log(collider.gameObject);
                return false;
            }
        }
        return true;
    }
}
