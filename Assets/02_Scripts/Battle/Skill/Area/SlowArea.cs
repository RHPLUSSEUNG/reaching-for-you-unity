using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : AreaInterface
{
    public override void CalcTurn()
    {
        GetCharacter();
        foreach (Collider collider in colliders)
        {
            Slow slow = new();
            if(collider.gameObject == Managers.Battle.currentCharacter)
            {
                slow.SetDebuff(1, collider.gameObject, 20, true);
            }
        }
        DestroyArea();
    }
}
