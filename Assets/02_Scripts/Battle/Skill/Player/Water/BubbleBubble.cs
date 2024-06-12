using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBubble : Active
{
    public override bool Activate()
    {
        for (int i = 0; i < 4; i++) 
        { 
            if(target.GetComponent<EntityStat>().Hp > 0)
            {
                Managers.Active.Damage(target, 10, element, false);
            }
            else
            {
                break;
            }
        }
        return true;
    }
}
