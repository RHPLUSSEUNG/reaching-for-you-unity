using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : Active
{
    GameObject[] targets = new GameObject[2];
    int healing;

    int compareDistance(GameObject character1, GameObject character2)
    {
        Vector3 curpos = target.transform.position;
        return Vector3.Distance(curpos, character1.transform.position) < Vector3.Distance(curpos, character1.transform.position) ? -1 : 1;
    }
    public override bool Activate()
    {
        SkillExtent extent = Managers.Skill.extent.GetComponent<SkillExtent>();
        List<GameObject> targets = extent.SetArea(range, target_object, Managers.Battle.currentCharacter.transform.position, false);
        targets.Sort(compareDistance);

        switch (targets.Count) 
        {
            case 0:
                return false;
            case 1:
                this.targets[0] = targets[0];
                break;
            default:
                this.targets[0] = targets[0];
                this.targets[1] = targets[1];
                break;
        }
        foreach (GameObject go in this.targets)
        {
            healing += Managers.Active.Damage(go, 20, element, false);
            Managers.Active.Heal(Managers.Battle.currentCharacter, healing*45/100);
        }
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        this.target = target;
        if (!Activate())
        {
            Debug.Log("Fail to Active Skill");
            return false;
        }
        
        foreach (GameObject go in targets)
        {
            Managers.Manager.StartCoroutine(Managers.Skill.StartEffect(Effect, go.transform.position));
        }
        
        return true;
    }
}
