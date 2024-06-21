using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public Dictionary<GameObject, int> consumeInven = new();
    public List<GameObject> equipmentInven = new();
    short inventoryCnt =  0;
    short inventoryMaxCnt = 100;
    int gold = 0;

    public bool AddItem(GameObject item, int num = 1)
    {
        if (item.GetComponent<Item>().type == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(item))
            {
                consumeInven[item] += num;
                return true;
            }
            else if (inventoryCnt < inventoryMaxCnt)
            {
                consumeInven.Add(item, 1);
                inventoryCnt++;
                return true;
            }
        }
        else if (item.GetComponent<Item>().type == ItemType.Equipment)
        {
            if (inventoryCnt < inventoryMaxCnt)
            {
                equipmentInven.Add(item);
                inventoryCnt++;
                return true;
            }
        }
        Debug.Log("Inventory is Already Full");
        return false;
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
    public bool EquipItem(GameObject item, GameObject player)
    {
        Equipment equip = item.GetComponent<Equipment>();
        Equip_Item equip_item = player.GetComponent<Equip_Item>();
        GameObject prev_item = null;
        if (equipmentInven.Contains(item) && equip != null)
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
                equipmentInven.Remove(item);
            }
        }
        return false;
    }

    public void ExchangeEquip(GameObject prev, GameObject cur, GameObject player)
    {
        cur.GetComponent<Equipment>().Equip(player);
        prev.GetComponent<Equipment>().UnEquip(player);
        equipmentInven.Remove(cur);
        equipmentInven.Add(prev);
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
        equipmentInven.Add(item);
        inventoryCnt++;
        return true;
    }
    #endregion

    #region EquipConsume
    public int EquipConsume(GameObject item, int Capacity)
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
}


