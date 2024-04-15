using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill1 : Active
{
    public override bool Activate(Vector3 pos)
    {
        Debug.Log("Skill1");
        return true;
    }

    public override bool SetPos()
    {
        Debug.Log("Skill1");
        return true;
    }
}
