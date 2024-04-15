using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory inven;
    public static Inventory Inven { get { return inven; } }
    #endregion
    public Dictionary<Consume, int> consumeInven = new Dictionary<Consume, int>();
    public List<Equipment> equipmentInven = new List<Equipment>();
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

    public bool AddItem(Item item)
    {
        if (item.type == ItemType.Consume)
        {
            if (consumeInven.ContainsKey(item as Consume))
            {
                consumeInven[item as Consume]++;
                return true;
            }
            else if (inventoryCnt < inventoryMaxCnt)
            {
                consumeInven.Add(item as Consume, 1);
                inventoryCnt++;
                return true;
            }
        }
        else if (item.type == ItemType.Equipment)
        {
            if (inventoryCnt < inventoryMaxCnt)
            {
                equipmentInven.Add(item as Equipment);
                inventoryCnt++;
                return true;
            }
        }
        Debug.Log("Inventory is Already Full");
        return false;
    }

    public bool ConsumeItem(Consume item)
    {
        if (consumeInven.ContainsKey(item as Consume))
        {
            consumeInven[item as Consume]--;
            if (consumeInven[item as Consume] == 0)
            {
                consumeInven.Remove(item as Consume);
                inventoryCnt--;
            }
            return true;
        }
        return false;
    }

    public bool EquipItem(Equipment item, PlayerSpec player)
    {
        if (equipmentInven.Contains(item))
        {
            if (player._equipItems[(int)item.part] != null)
            {
                player._equipItems[(int)item.part] = item;
                item.Equip();
                equipmentInven.Remove(item);
                return true;
            }
            ExchangeEquip(player._equipItems[(int)item.part], item, player);
            return true;
        }
        return false;
    }

    public void ExchangeEquip(Equipment prev, Equipment cur, PlayerSpec player)
    {
        Equipment temp = prev;
        prev.UnEquip();
        player._equipItems[(int)prev.part] = cur;
        cur.Equip();
        equipmentInven.Remove(cur);
        equipmentInven.Add(prev);
    }

    public bool UnEquipItem(Equipment item, PlayerSpec player)
    {
        if (inventoryCnt == inventoryMaxCnt)
        {
            Debug.Log("Inventory is Already Full");
            return false;
        }
        player._equipItems[(int)item.part] = null;
        equipmentInven.Add(item);
        inventoryCnt++;
        return true;
    }
}
