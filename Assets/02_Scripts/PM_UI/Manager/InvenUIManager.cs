using System.Collections;
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

    public void SetPlayerInvenUI()
    {
        SetPlayerHeadUI();
        SetPlayerBodyUI();
        SetPlayerWeaponUI();
        SetPlayerConsumeUI();
    }
    void SetPlayerHeadUI()
    {
        Image equip = Util.FindChild(head.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Head == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Head.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    void SetPlayerBodyUI()
    {
        Image equip = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Upbody == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Upbody.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    void SetPlayerWeaponUI()
    {
        Image equip = Util.FindChild(weapon.gameObject, "EquipIcon").GetComponent<Image>();
        if (player.GetComponent<Equip_Item>().Weapon == null)
        {
            return;
        }
        Image itemIcon = player.GetComponent<Equip_Item>().Weapon.GetComponent<Image>();
        equip.sprite = itemIcon.sprite;
    }

    void SetPlayerConsumeUI()
    {

    }

    public void CreateEquipUI()
    {
        equipUI = PM_UI_Manager.UI.CreatePopupUI<EquipUI>("EquipUI");
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
                case EquipPart.UpBody:
                    equipIcon = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
                    if (player.GetComponent<Equip_Item>().Upbody == null)
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
        Managers.Item.EquipItem(focusItem.GetComponent<InvenItemUI>().invenItem, player);
        equipIcon.sprite = changeIcon.sprite;       /// 아이템 데이터를 적용
        PM_UI_Manager.Resource.Destroy(focusItem);
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
            case EquipPart.UpBody:
                equipIcon = Util.FindChild(body.gameObject, "EquipIcon").GetComponent<Image>();
                prev_Item = player.GetComponent<Equip_Item>().Upbody;
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
        InvenItemUI itemUI = PM_UI_Manager.UI.MakeSubItem<InvenItemUI>(invenContent.transform, "InvenItem");
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

        // 2번째 방법(생성과 삭제가 적은 방법, 에러 발생으로 보완 필요)
        // Image swapIcon = player.GetComponent<Equip_Item>().Weapon.GetComponent<Image>();
        // Managers.Item.EquipItem(focusItem.GetComponent<InvenItemUI>().invenItem, player);
        // equipIcon.sprite = changeIcon.sprite;
        // InvenItemUI focusItemUI = focusItem.GetComponent<InvenItemUI>();
        // focusItemUI.SetItemInfo(swapIcon);
    }

    public void ConsumeInvenUI()
    {

    }
}
