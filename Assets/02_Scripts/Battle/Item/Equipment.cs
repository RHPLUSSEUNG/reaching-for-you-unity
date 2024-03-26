using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Item
{
    public EquipPart part;
    public ElementType elementType;
    public abstract bool Equip();
    public abstract bool UnEquip();
}
