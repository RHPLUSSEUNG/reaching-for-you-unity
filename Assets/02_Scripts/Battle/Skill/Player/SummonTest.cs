using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTest : Active
{
    public override bool Activate()
    {
        GameObject obj = Managers.Party.InstantiateMonster("Enemy_Worker");
        obj.transform.position = target.transform.position + new Vector3(0, 1, 0);
        return true;
    }
}
