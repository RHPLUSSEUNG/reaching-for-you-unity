using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemData : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public ItemType ItemType;
    public int reqLev;

    public EquipPart part;
    public ElementType element;

    public TargetObject target;
}