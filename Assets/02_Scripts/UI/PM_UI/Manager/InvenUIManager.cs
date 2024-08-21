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
    public EquipUI equipUI = null;
    public ItemType type;
    public EquipPart part;
    public bool inven_state = false;

    //test
    public Sprite[] test_sprite = new Sprite[3];

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
        for (int i = 0; i< Managers.Item.equipmentInven.Count; i++)
        {
            EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
            equipItem.invenItemID = Managers.Item.equipmentInven[i];
            equipItem.SetItemInfo();
            Managers.UI.HideUI(equipItem.gameObject);
        }
        foreach (KeyValuePair<int, int> consume in Managers.Item.consumeInven)
        {
            ConsumeItemUI consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
            consumeItem.invenItemID = consume.Key;
            consumeItem.SetItemInfo(test_sprite[consume.Key], consume.Value);      // test 스프라이트 적용
            Managers.UI.HideUI(consumeItem.gameObject);
        }
    }

    public void TempInvenUI(int itemID)       // 아이템 획득 시 인벤토리 최신화(생성 후)
    {
        ItemData itemData = Managers.Data.GetItemData(itemID);
        if(itemData.ItemType == ItemType.Equipment)
        {
            EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
            equipItem.invenItemID = itemID;
            equipItem.SetItemInfo();
            Managers.UI.HideUI(equipItem.gameObject);
        }
        else
        {
            Managers.Item.consumeInven.TryGetValue(itemID, out int count);      // ContainsKey 함수를 TryGetValue로 바꾸기
            Debug.Log($"Item ID : {itemID}, value : {count}");  
            if(Managers.Item.consumeInven.ContainsKey(itemID))
            {
                for (int i = 0; i < invenContent.transform.childCount; i++)
                {
                    InvenItemUI InvenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                    if(itemID == InvenItem.invenItemID)
                    {
                        ConsumeItemUI consumeItem = invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>();
                        consumeItem.UpdateConsumeValue(Managers.Item.consumeInven[itemID]);
                    }
                }
            }
            else
            {
                ConsumeItemUI consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
                consumeItem.invenItemID = itemID;
                consumeItem.SetItemInfo(test_sprite[itemID], Managers.Item.consumeInven[itemID]);      // test 스프라이트 적용
                Managers.UI.HideUI(consumeItem.gameObject);
            }
        }
    }

    public void UpdateInvenUI()
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
            // ConsumeEquipUI(itemID, capacity)
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
            Debug.Log($"player : {player}");
            Debug.Log($"prev_item : {prev_Item}");
            return;
        }
        EquipItemUI itemUI = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
        Image unequipIcon = prev_Item.GetComponent<Image>();
        itemUI.invenItem = prev_Item;
        itemUI.SetItemInfo();        // 아이템 데이터를 적용

        Managers.Item.UnEquipItem(prev_Item, player);
        equipIcon.sprite = null;
    }

    public void ExchangeInvenUI()
    {
        UnEquipInvenUI();
        EquipInvenUI();

    }

    public void ConsumeEquipUI(int itemID, int capacity)
    {
        if(Managers.Item.EquipConsume(itemID, capacity) <= 0)
        {
            for (int i = 0; i < invenContent.transform.childCount; i++)
            {
                InvenItemUI InvenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                if (itemID == InvenItem.invenItemID)
                {
                    GameObject removeItem = invenContent.transform.GetChild(i).gameObject;
                    Managers.Prefab.Destroy(removeItem);
                }
            }
        }
        else
        {
            for (int i = 0; i < invenContent.transform.childCount; i++)
            {
                InvenItemUI InvenItem = invenContent.transform.GetChild(i).GetComponent<InvenItemUI>();
                if (itemID == InvenItem.invenItemID)
                {
                    ConsumeItemUI consumeItem = invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>();
                    consumeItem.UpdateConsumeValue(Managers.Item.consumeInven[itemID]);
                }
            }
        }
    }

    public void CreateEquipUI()
    {
        equipUI = Managers.UI.CreatePopupUI<EquipUI>("EquipUI");
    }
}
