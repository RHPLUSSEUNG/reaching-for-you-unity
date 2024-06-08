using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenUIManager
{
    public Image head;
    public Image body;
    public Image weapon;
    public Image equipIcon;
    public Image changeIcon;
    public GameObject invenContent;
    public GameObject player;
    public GameObject focusItem;
    public EquipUI equipUI = null;
    public ItemType type;
    public EquipPart part;

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

    }

    public void SetInventory()
    {
        for (int i = 0; i< Managers.Item.equipmentInven.Count; i++)
        {
            EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
            equipItem.invenItem = Managers.Item.equipmentInven[i];
            // equipItem.SetItemInfo();  아이템 정보 가져오기
            Managers.UI.HideUI(equipItem.gameObject);
        }
        foreach (KeyValuePair<GameObject, int> consume in Managers.Item.consumeInven)
        {
            ConsumeRepairUI consumeItem = Managers.UI.MakeSubItem<ConsumeRepairUI>(invenContent.transform, "ConsumeRepairUI");
            Image icon = consume.Key.GetComponent<Image>();
            consumeItem.SetInfo(icon, consume.Value);
        }
    }

    public void CreateEquipUI()
    {
        equipUI = Managers.UI.CreatePopupUI<EquipUI>("EquipUI");
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
            // 소모품 인벤
        }
        
    }

    public void EquipInvenUI()
    {
        Managers.Item.EquipItem(focusItem.GetComponent<EquipItemUI>().invenItem, player);
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
            return;
        }
        EquipItemUI itemUI = Managers.UI.MakeSubItem<EquipItemUI>(invenContent.transform, "EquipItem");
        Image unequipIcon = prev_Item.GetComponent<Image>();
        itemUI.invenItem = prev_Item;
        itemUI.SetItemInfo(unequipIcon);        // 아이템 데이터를 적용

        Managers.Item.UnEquipItem(prev_Item, player);
        equipIcon.sprite = null;
    }

    public void ExchangeInvenUI()
    {
        UnEquipInvenUI();
        EquipInvenUI();

    }

    public void ConsumeEquipUI()
    {

    }
}
