using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestAttack : Active
{
    GameObject target;
    public override bool Activate()
    {
        Managers.Party.Damage(target);
        Debug.Log("Player Attack");
        Managers.Battle.NextTurn();
        return true;
    }

    public override bool SetTarget(GameObject target)
    {
        this.target = target;
        Activate();
        return true;
    }
}
