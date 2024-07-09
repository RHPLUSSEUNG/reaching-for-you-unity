using UnityEngine;

public abstract class Equipment : Item
{
    public EquipPart part;
    public ElementType elementType;
    public abstract bool Equip(GameObject character);
    public abstract bool UnEquip(GameObject character);
    

    public override void Start()
    {
        Managers.Data.SetItem(itemId, this);
    }
}
