using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Consume")]
public class ConsumeData : ItemData
{
    public TargetObject target;
    public int maxCapacity;
}
