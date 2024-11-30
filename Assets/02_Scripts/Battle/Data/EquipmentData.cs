using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Equipment")]
public class EquipmentData : ItemData
{
    public EquipPart part;
    public ElementType element;
}
