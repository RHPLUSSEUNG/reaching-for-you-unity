using System.Collections.Generic;
using UnityEngine;

public class Equip_Item : MonoBehaviour
{
    public GameObject Head;
    public GameObject Body;
    public GameObject Weapon;


    public int consume_cnt = 6;
    public Dictionary<GameObject, int> Consumes = new();

    public void AddConsume(GameObject item, int capcity)
    {
        int num = Managers.Item.ConsumeItem(item, capcity);
        if (Consumes.ContainsKey(item))
        {
            Consumes[item] += num;
            return;
        }
        Consumes.Add(item, num);
        return;
    }
}
