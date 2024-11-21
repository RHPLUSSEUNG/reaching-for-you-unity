using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenUIManager
{
    public Image head;
    public Image body;
    public Image weapon;
    public GameObject consumeLayout;

    public Image equipIcon;
    public Image changeIcon;
    public GameObject invenUI;
    public GameObject invenContent;
    public GameObject player;
    public GameObject focusItem;
    public int focusItemID;
    public EquipUI equipUI = null;
    public ItemType type;
    public EquipPart part;
    // public bool inven_state = false;
    public bool consume_equip_state = false;
    public Sprite emptySprite;

    List<int> equip_consume_ui = new List<int>();

    #region Set Character Equip Info
    public void SetPlayerEquipUI(Equip_Item equipInfo)
    {
        SetPlayerHeadUI(equipInfo);
        SetPlayerBodyUI(equipInfo);
        SetPlayerWeaponUI(equipInfo);
        SetPlayerConsumeUI(equipInfo);
    }
    private void SetPlayerHeadUI(Equip_Item equipInfo)
    {
        Image equip = Util.FindChild(head.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Head == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Head.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    private void SetPlayerBodyUI(Equip_Item equipInfo)
    {
        Image equip = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Body == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Body.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    private void SetPlayerWeaponUI(Equip_Item equipInfo)
    {
        Image equip = Util.FindChild(weapon.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Weapon == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Weapon.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    private void SetPlayerConsumeUI(Equip_Item equipInfo)
    {
        if (player.GetComponent<Equip_Item>().Consumes.Count == 0)
        {
            return;
        }
        for (int i = 0; i < consumeLayout.transform.childCount; i++)
        {
            GameObject consume = consumeLayout.transform.GetChild(i).gameObject;
            Image equip = Util.FindChild(consume, "EquipIcon").GetComponent<Image>();

        }
    }
    #endregion

    public void SetInventory()
    {
        for (int i = 0; i< Managers.Item.GetWeaponList().Count; i++)
        {
            EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
            equipItem.invenItemID = Managers.Item.GetWeaponList()[i];
            equipItem.SetItemInfo();
            Managers.UI.HideUI(equipItem.gameObject);
        }
        foreach (KeyValuePair<int, int> consume in Managers.Item.consumeInven)
        {
            ConsumeItemUI consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
            consumeItem.invenItemID = consume.Key;
            consumeItem.SetItemInfo(consume.Value);      // test 스프라이트 적용
            Managers.UI.HideUI(consumeItem.gameObject);
        }
    }

    public void UpdateItemUI(int itemID)       // 아이템 획득 시 인벤토리 최신화(생성 후)
    {
        ItemData itemData = (ItemData)Managers.Data.ParsingData(itemID);
        if(itemData.ItemType == ItemType.Equipment)
        {
            EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
            equipItem.invenItemID = itemID;
            equipItem.SetItemInfo();
            Managers.UI.HideUI(equipItem.gameObject);
        }
        else
        {
            ConsumeItemUI consumeItem;
            if(Managers.Item.consumeInven.TryGetValue(itemID, out int count))
            {
                for (int i = 0; i < invenContent.transform.childCount; i++)
                {
                    InvenItemUI InvenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                    if(itemID == InvenItem.invenItemID)
                    {
                        consumeItem = invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>();
                        consumeItem.UpdateConsumeValue(count);
                        return;
                    }
                }
            }
            consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
            consumeItem.invenItemID = itemID;
            consumeItem.SetItemInfo(count);      // test 스프라이트 적용
            Managers.UI.HideUI(consumeItem.gameObject);
        }
    }

    public void UpdateInvenUI(int count = 1)
    {
        if(type == ItemType.Equipment)
        {
            switch (part)
            {
                case EquipPart.Head:
                    equipIcon = Util.FindChild(head.gameObject, "EquipIcon").GetComponent<Image>();
                    if (player.GetComponent<Equip_Item>().Head == null)
                    {
                        EquipInvenUI();
                    }
                    else
                    {
                        ExchangeInvenUI();
                    }
                    break;
                case EquipPart.Body:
                    equipIcon = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
                    if (player.GetComponent<Equip_Item>().Body == null)
                    {
                        EquipInvenUI();
                    }
                    else
                    {
                        ExchangeInvenUI();
                    }
                    break;
                case EquipPart.Weapon:
                    equipIcon = Util.FindChild(weapon.gameObject, "EquipIcon").GetComponent<Image>();
                    if (player.GetComponent<Equip_Item>().Weapon == null)
                    {
                        EquipInvenUI();
                    }
                    else
                    {
                        ExchangeInvenUI();
                    }
                    break;
            }
        }
        else
        {          
            if(consume_equip_state)
            {
                ConsumeEquipUI(count);
            }
            else
            {
                ConsumeUnEquipUI(count);
            }
        }

    }

    public void EquipInvenUI()
    {
        Managers.Item.EquipItem(focusItem.GetComponent<EquipItemUI>().invenItemID, player);
        equipIcon.sprite = changeIcon.sprite;       /// 아이템 데이터를 적용
        Managers.Prefab.Destroy(focusItem);
    }

    public void UnEquipInvenUI()
    {
        GameObject prev_Item = null;
        switch (part)
        {
            case EquipPart.Head:
                equipIcon = Util.FindChild(head.gameObject, "EquipIcon").GetComponent<Image>();
                prev_Item = player.GetComponent<Equip_Item>().Head;
                break;
            case EquipPart.Body:
                equipIcon = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
                prev_Item = player.GetComponent<Equip_Item>().Body;
                break;
            case EquipPart.Weapon:
                equipIcon = Util.FindChild(weapon.gameObject, "EquipIcon").GetComponent<Image>();
                prev_Item = player.GetComponent<Equip_Item>().Weapon;
                break;
        }
        if (prev_Item == null)
        {
            Debug.Log("prev_Item Null");
            return;
        }

        Equipment equipItem = prev_Item.GetComponent<Equipment>();
        EquipItemUI itemUI = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");        
        itemUI.invenItemID = equipItem.itemId;
        itemUI.invenItem = prev_Item;
        itemUI.SetItemInfo();        // 아이템 데이터를 적용

        Managers.Item.UnEquipItem(prev_Item, player);
        equipIcon.sprite = emptySprite;
    }

    public void ExchangeInvenUI()
    {
        UnEquipInvenUI();
        EquipInvenUI();

    }

    public void ConsumeEquipUI(int capacity = 1)
    {
        Equip_Item character_equip = player.GetComponent<Equip_Item>();
        if (character_equip == null)
        {
            Debug.Log("Character_Equip Error");
            return;
        }

        if(capacity == 0)
        {
            return;
        }

        character_equip.AddConsume(focusItemID, capacity);
        if(!equip_consume_ui.Contains(focusItemID))
        {
            equip_consume_ui.Add(focusItemID);
        }
        // TODO : equip_conume_ui 초기화(플레이어 변경 시), equip_cousume_ui 총 용량 예외 처리 필요성 고려

        int index = 0;
        for(int i = 0; i < equip_consume_ui.Count; i++)
        {
            if (equip_consume_ui[i] == focusItemID)
            {
                index = i;
            }
        }
        invenUI.GetComponent<InvenUI>().ConsumeEquipedUI(index, focusItemID);
        Debug.Log("Equip Success");

        
        if (Managers.Item.consumeInven.TryGetValue(focusItemID, out int remainValue))
        {
            focusItem.GetComponent<ConsumeItemUI>().UpdateConsumeValue(remainValue);
        }
        else
        {
            focusItem.GetComponent<ConsumeItemUI>().UpdateConsumeValue(0);
        }
    }

    public void ConsumeUnEquipUI(int capacity = 1)
    {
        Equip_Item character_equip = player.GetComponent<Equip_Item>();
        if (character_equip == null)
        {
            Debug.Log("Character_Equip Error");
            return;
        }

        if (capacity == 0)
        {
            return;
        }

        character_equip.DeleteConsume(focusItemID);
        equip_consume_ui.Remove(focusItemID);
        Debug.Log("UnEquip Success");
        // TODO : 특정 갯수만 UnEquip
        ConsumeItemUI consumeItem;
        if (Managers.Item.consumeInven.TryGetValue(focusItemID, out int remainValue))
        {
            for(int i = 0; i < invenContent.transform.childCount; i++)
            {
                InvenItemUI invenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                if(focusItemID == invenItem.invenItemID)
                {
                    consumeItem = invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>();
                    consumeItem.UpdateConsumeValue(remainValue);
                    return;
                }
            }
        }
        consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
        consumeItem.invenItemID = focusItemID;
        consumeItem.SetItemInfo(remainValue);
        Managers.UI.HideUI(consumeItem.gameObject);
        invenUI.GetComponent<InvenUI>().MoveConsumeTab();
    }

    public void RemoveItemUI()
    {
        ItemData itemData = (ItemData)Managers.Data.ParsingData(focusItemID);
        if(itemData.ItemType == ItemType.Consume)
        {
            ConsumeItemUI consumeItem;
            if (Managers.Item.consumeInven.TryGetValue(focusItemID, out int remainValue))
            {
                for (int i = 0; i < invenContent.transform.childCount; i++)
                {
                    InvenItemUI invenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                    if (focusItemID == invenItem.invenItemID)
                    {
                        consumeItem = invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>();
                        consumeItem.UpdateConsumeValue(remainValue);
                        return;
                    }
                }
            }
        }
        Managers.Prefab.Destroy(focusItem);
    }

    public void CreateEquipUI()
    {
        equipUI = Managers.UI.CreatePopupUI<EquipUI>("EquipUI");
    }
}
