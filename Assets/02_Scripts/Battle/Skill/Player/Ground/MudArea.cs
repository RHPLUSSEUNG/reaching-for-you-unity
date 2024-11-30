using UnityEngine;

public class MudArea : Active
{
    public override bool Activate()
    {
        //TODO Effect
        GameObject area = Managers.Prefab.Instantiate("Area/SlowArea");
        area.GetComponent<SlowArea>().SetArea(7, 3, TargetObject.Enemy, target.transform.position);
        return true;
    }
}