using System.Collections.Generic;
using UnityEngine;

public class Equip_Item : MonoBehaviour
{
    public GameObject Head;
    public GameObject Body;
    public GameObject Weapon;


    public int consume_cnt = 6;
    public Dictionary<GameObject, int> Consumes = new();

    #region Consume
    public void AddConsume(GameObject item, int capcity)
    {
        if(Consumes.Count >= consume_cnt)
        {
            Debug.Log("Full Consume");
            return;
        }
        int num = Managers.Item.EquipConsume(item, capcity);
        if (Consumes.ContainsKey(item))
        {
            Consumes[item] += num;
            return;
        }
        Consumes.Add(item, num);
        return;
    }

    public void DeleteConsume(GameObject item)
    {
        if(Managers.Item.AddItem(item, Consumes[item]))
        {
            Consumes.Remove(item);
            return;
        }
        Debug.Log("Can't Delete Consume");
        return;
    }

    public void UseConsume(GameObject item, GameObject target)
    {
        if(Consumes.ContainsKey(item)) 
        {
            item.GetComponent<Consume>().Activate(target);
            Consumes[item]--;
            if (Consumes[item] == 0)
            {
                Consumes.Remove(item);
            }
            return;
        }
        Debug.Log("There is No item");
        return;
    }
    #endregion
}
