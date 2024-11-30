using System.Collections.Generic;
using UnityEngine;

public class Equip_Item : MonoBehaviour
{
    public GameObject Head;
    public GameObject Body;
    public GameObject Weapon;


    public int consume_cnt = 6;
    // <id , capacity>
    public Dictionary<int, int> Consumes = new();

    #region Consume
    public void AddConsume(int itemID, int capcity)
    {
        if(Consumes.Count >= consume_cnt)
        {
            Debug.Log("Full Consume");
            return;
        }
        int num = Managers.Item.EquipConsume(itemID, capcity);
        if (Consumes.ContainsKey(itemID))
        {
            Consumes[itemID] += num;
            return;
        }
        Consumes.Add(itemID, num);
        return;
    }

    public void DeleteConsume(int itemID)
    {
        if(Managers.Item.AddItem(itemID, Consumes[itemID]))
        {
            Consumes.Remove(itemID);
            return;
        }
        Debug.Log("Can't Delete Consume");
        return;
    }

    public void UseConsume(int itemID, GameObject target)
    {
        if(Consumes.ContainsKey(itemID)) 
        {
            GameObject item = Managers.Item.InstantiateConsumeItem(itemID);
            item.GetComponent<Consume>().Activate(target);
            Consumes[itemID]--;
            if (Consumes[itemID] == 0)
            {
                Consumes.Remove(itemID);
            }
            return;
        }
        Debug.Log("There is No item");
        return;
    }
    #endregion
}
