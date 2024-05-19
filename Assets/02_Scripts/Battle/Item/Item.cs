using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int itemId; //item id
    public ItemType type; //item type (equipment, consume)
    public int reqLev; //item require level

    public abstract void Start();
}
