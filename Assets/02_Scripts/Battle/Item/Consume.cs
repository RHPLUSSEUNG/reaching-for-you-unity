using UnityEngine;

public abstract class Consume : Item
{
    public TargetObject targetObject;
    public int maxCapacity;
    public int range;

    public abstract bool Activate(GameObject target); //consume item

    public override void Start()
    {
        Managers.Data.SetItem(itemId, this);
    }
}
