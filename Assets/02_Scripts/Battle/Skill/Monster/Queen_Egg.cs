using UnityEngine;

public class Queen_Egg : Active
{
    public override bool Activate()
    {
        GameObject obj = Managers.Party.InstantiateMonster("Enemy_Worker");
        obj.transform.position = target.transform.position + new Vector3(0, 1, 0);
        return true;
    }
}
