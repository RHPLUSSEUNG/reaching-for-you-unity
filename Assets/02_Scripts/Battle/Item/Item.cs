using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId; //item id
    public ItemType type; //item type (equipment, consume)
    public short reqLev; //item require level
}
