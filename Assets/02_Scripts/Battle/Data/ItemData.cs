using UnityEngine;


public class ItemData : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public string ItemDescription;
    public ItemType ItemType;
    public int reqLev;
    public Sprite itemSprite;
    public int price;
    public int maxStackSize = 99;
}