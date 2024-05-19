using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    public Dictionary<GameObject, int> consumeInven = new();
    public List<GameObject> equipmentInven = new();
    short inventoryCnt =  0;
    short inventoryMaxCnt = 100;
    int gold = 0;

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

    public bool AddItem(GameObject item)
    {
        if (item.GetComponent<Item>().type == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(item))
            {
                consumeInven[item]++;
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

    public bool ConsumeItem(GameObject item)
    {
        if (consumeInven.ContainsKey(item))
        {
            consumeInven[item]--;
            if (consumeInven[item] == 0)
            {
                consumeInven.Remove(item);
                inventoryCnt--;
            }
            return true;
        }
        return false;
    }

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
                    prev_item = equip_item.Upbody;
                    equip_item.Upbody = item;
                    break;
                case (EquipPart)2:
                    prev_item = equip_item.Downbody;
                    equip_item.Downbody = item;
                    break;
                case (EquipPart)3:
                    prev_item = equip_item.Weapon;
                    equip_item.Weapon = item;
                    break;
            }

            if(prev_item != null)
            {
                ExchangeEquip(prev_item, item);
            }
            else
            {
                equip.Equip();
                equipmentInven.Remove(item);
            }
        }
        return false;
    }

    public void ExchangeEquip(GameObject prev, GameObject cur)
    {
        cur.GetComponent<Equipment>().Equip();
        prev.GetComponent<Equipment>().UnEquip();
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
                equip_item.Upbody = null;
                break;
            case (EquipPart)2:
                equip_item.Downbody = null;
                break;
            case (EquipPart)3:
                equip_item.Weapon = null;
                break;
        }

        equipmentInven.Add(item);
        inventoryCnt++;
        return true;
    }
}
