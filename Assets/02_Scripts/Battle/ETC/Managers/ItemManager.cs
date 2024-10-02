using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public Dictionary<int, int> consumeInven = new();
    public List<int> equipmentInven = new();
    short inventoryCnt =  0;
    short inventoryMaxCnt = 100;
    int gold = 0;    

    //[2024-09-13][LSH's Code]: [quest-objective-gather]
    public bool AddItem(int itemID, int num = 1)
    {
        ItemData itemData = Managers.Data.GetItemData(itemID);
        if (itemData.ItemType == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(itemID))
            {
                consumeInven[itemID] += num;
                ObjectiveTracer.Instance.ReportIItemCollected(itemID);                      
                return true;
            }
            else if (inventoryCnt < inventoryMaxCnt)
            {
                //[2024-10-02][LSH's Code]: [prototype-5]
                consumeInven.Add(itemID, num);
                inventoryCnt++;                
                ObjectiveTracer.Instance.ReportIItemCollected(itemID);                
                return true;
            }
        }
        else if (itemData.ItemType == ItemType.Equipment)
        {
            if (inventoryCnt < inventoryMaxCnt)
            {
                equipmentInven.Add(itemID);
                inventoryCnt++;                
                ObjectiveTracer.Instance.ReportIItemCollected(itemID);                
                return true;
            }
        }        
        Debug.Log("Inventory is Already Full");
        return false;
    }

    //[2024-09-13][LSH's Code]: [quest-objective-gather]
    public bool RemoveItem(int itemID, int num = 1)
    {
        ItemData itemData = Managers.Data.GetItemData(itemID);
        if (itemData.ItemType == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(itemID) && consumeInven[itemID] >= num)
            {
                consumeInven[itemID] -= num;
                if(consumeInven[itemID] <= 0)
                {
                    consumeInven.Remove(itemID);
                }                
                inventoryCnt--;                
                ObjectiveTracer.Instance.ReportIItemCollected(itemID);
                return true;
            }
        }
        else if (itemData.ItemType == ItemType.Equipment)
        {
            if (equipmentInven.Contains(itemID))
            {
                equipmentInven.Remove(itemID);
                inventoryCnt--;                
                ObjectiveTracer.Instance.ReportIItemCollected(itemID);
                return true;
            }
        }                   
        return false;
    }

    //[2024-09-16][LSH's Code]: [quest-objective-gather]
    public int SearchItem(int itemID)
    {
        ItemData itemData = Managers.Data.GetItemData(itemID);
        if (itemData.ItemType == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(itemID))
            {
                return consumeInven[itemID];
            }
        }
        else if (itemData.ItemType == ItemType.Equipment)
        {
            int equipCount = 0;
            for (int i = 0; i < equipmentInven.Count; i++)
            {
                if (equipmentInven[i] == itemID)
                {
                    equipCount++;
                }
            }

            return equipCount;
        }
        return 0;
    }

    public GameObject InstantiateConsumeItem(int itemID)
    {
        GameObject Item = Managers.Prefab.Instantiate($"Item/Consume/{Managers.Data.GetItemData(itemID).name}");
        return Item;
    }

    public GameObject InstantiateEquipmentItem(int itemID)
    {
        GameObject Item = Managers.Prefab.Instantiate($"Item/Equipment/{Managers.Data.GetItemData(itemID).name}");
        return Item;
    }

    #region gold
    public bool getGold(short gold)
    {
        this.gold += gold;
        return true;
    }

    public bool useGold(short gold)
    {
        if(this.gold < gold)
        {
            return false;
        }
        this.gold -= gold;
        return true;
    }

    #endregion

    #region Equipment equip
    public bool EquipItem(int itemID, GameObject player)
    {
        GameObject item = InstantiateEquipmentItem(itemID);
        Equipment equip = item.GetComponent<Equipment>();
        Equip_Item equip_item = player.GetComponent<Equip_Item>();
        GameObject prev_item = null;
        if (equipmentInven.Contains(itemID) && equip != null)
        {
            switch(equip.part) 
            {
                case (EquipPart)0:
                    prev_item = equip_item.Head;
                    equip_item.Head = item;
                    break;
                case (EquipPart)1:
                    prev_item = equip_item.Body;
                    equip_item.Body = item;
                    break;
                case (EquipPart)2:
                    prev_item = equip_item.Weapon;
                    equip_item.Weapon = item;
                    break;
            }

            if(prev_item != null)
            {
                ExchangeEquip(prev_item, item, player);
            }
            else
            {
                equip.Equip(player);
                equipmentInven.Remove(itemID);
            }
        }
        return false;
    }

    public void ExchangeEquip(GameObject prev, GameObject cur, GameObject player)
    {
        cur.GetComponent<Equipment>().Equip(player);
        prev.GetComponent<Equipment>().UnEquip(player);
        equipmentInven.Remove(cur.GetComponent<Equipment>().itemId);
        equipmentInven.Add(cur.GetComponent<Equipment>().itemId);
    }

    public bool UnEquipItem(GameObject item, GameObject player)
    {
        if (inventoryCnt == inventoryMaxCnt)
        {
            Debug.Log("Inventory is Already Full");
            return false;
        }
        Equipment unequip = item.GetComponent<Equipment>();

        Equip_Item equip_item = player.GetComponent<Equip_Item>();
        switch (unequip.part)
        {
            case (EquipPart)0:
                equip_item.Head = null;
                break;
            case (EquipPart)1:
                equip_item.Body = null;
                break;
            case (EquipPart)2:
                equip_item.Weapon = null;
                break;
        }
        item.GetComponent<Equipment>().UnEquip(player);
        equipmentInven.Add(item.GetComponent<Equipment>().itemId);
        inventoryCnt++;
        return true;
    }
    #endregion

    #region EquipConsume
    public int EquipConsume(int item, int Capacity)
    {
        int num = 0;
        if (consumeInven.ContainsKey(item))
        {
            if (consumeInven[item] - Capacity <= 0)
            {
                num = consumeInven[item];
                consumeInven.Remove(item);
                inventoryCnt--;
                return num;
            }
            consumeInven[item] -= Capacity;
            return Capacity;
        }
        return 0;
    }

    #endregion

    //[2024-09-24][LSH's Code]: [gift-function]
    public Dictionary<int, int> GetConsumeItem()
    {
        return consumeInven;
    }
    
    public List<int> GetEquipmentItem()
    {
        return equipmentInven;
    }
}


