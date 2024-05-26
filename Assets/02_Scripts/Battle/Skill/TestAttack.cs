using UnityEngine;

public class TestAttack : Active
{
    public override bool Activate()
    {
        Managers.Active.Damage(target);
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
