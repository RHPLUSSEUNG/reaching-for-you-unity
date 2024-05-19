public abstract class Equipment : Item
{
    public EquipPart part;
    public ElementType elementType;
    public abstract bool Equip();
    public abstract bool UnEquip();

    public override void Start()
    {
        Managers.Data.SetItem(itemId, this);
    }
}
