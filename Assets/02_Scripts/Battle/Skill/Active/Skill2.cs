using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill2 : Active
{
    public override bool Activate(Vector3 pos)
    {
        Debug.Log("Skill2");
        return true;
    }

    public override bool SetPos()
    {
        Debug.Log("Skill2");
        return true;
    }
}
