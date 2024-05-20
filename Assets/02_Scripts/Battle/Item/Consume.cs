using UnityEngine;

public abstract class Consume : Item
{
    public abstract bool Activate(GameObject target); //consume item

    public override void Start()
    {
        Managers.Data.SetItem(itemId, this);
    }
}
