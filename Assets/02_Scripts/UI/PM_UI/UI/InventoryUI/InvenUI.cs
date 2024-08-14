using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenUI : UI_Base
{
    enum invenUI
    {
        HeadEquip,
        BodyEquip,
        WeaponEquip,
        InvenContent,
        ConsumeLayout,
        CharacterLayout,
        WeaponTab,
        ArmorTab,
        ConsumeTab,
    }

    [SerializeField] Sprite activeTab;
    [SerializeField] Sprite disabledTab;

    RectTransform weaponRect;
    RectTransform armorRect;
    RectTransform consumeRect;

    float activeTabXSize = 110.0f;
    float disableTabXSize = 100.0f;
    float activeTabYSize = 80.0f;
    float disableTabYSize = 65.0f;

    public override void Init()
    {
        Bind<GameObject>(typeof(invenUI));

        Managers.InvenUI.head = GetObject((int)invenUI.HeadEquip).GetComponent<Image>();
        Managers.InvenUI.body = GetObject((int)invenUI.BodyEquip).GetComponent<Image>();
        Managers.InvenUI.weapon = GetObject((int)invenUI.WeaponEquip).GetComponent<Image>();
        Managers.InvenUI.consumeLayout = GetObject((int)invenUI.ConsumeLayout).GetComponent<GameObject>();
        Managers.InvenUI.invenContent = GetObject((int)invenUI.InvenContent);

        BindEvent(GetObject((int)invenUI.HeadEquip), HeadEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.BodyEquip), BodyEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.WeaponEquip), WeaponEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.WeaponTab), WeaponTabClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.ArmorTab), ArmorTabClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.ConsumeTab), ConsumeTabClick, Define.UIEvent.Click);

        weaponRect = GetObject((int)invenUI.WeaponTab).GetComponent<RectTransform>();
        armorRect = GetObject((int)invenUI.ArmorTab).GetComponent<RectTransform>();
        consumeRect = GetObject((int)invenUI.ConsumeTab).GetComponent<RectTransform>();

        Managers.InvenUI.SetInventory();
    }

    public void HeadEquipButtonClick(PointerEventData data)
    {
        MoveArmorTab();
        Managers.InvenUI.part = EquipPart.Head;
        Managers.InvenUI.UnEquipInvenUI();
    }
    public void BodyEquipButtonClick(PointerEventData data)
    {
        MoveArmorTab();
        Managers.InvenUI.part = EquipPart.Body;
        Managers.InvenUI.UnEquipInvenUI();
    }

    public void WeaponEquipButtonClick(PointerEventData data)
    {
        MoveWeaponTab();
        Managers.InvenUI.part = EquipPart.Weapon;
        Managers.InvenUI.UnEquipInvenUI();
    }


    public void WeaponTabClick(PointerEventData data)
    {
        MoveWeaponTab();
    }
    public void MoveWeaponTab()
    {
        if (Managers.InvenUI.equipUI != null)
        {
            Managers.UI.ClosePopupUI(Managers.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = activeTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = disabledTab;
        for (int i = 0; i < Managers.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = Managers.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Equipment && invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.Weapon)
            {
                Managers.UI.ShowUI(invenItem);
            }
            else
            {
                Managers.UI.HideUI(invenItem);
            }
        }

        Vector2 weaponTabSize = weaponRect.sizeDelta;
        weaponTabSize.x = activeTabXSize;
        weaponTabSize.y = activeTabYSize;
        weaponRect.sizeDelta = weaponTabSize;
        Vector2 armorTabSize = armorRect.sizeDelta;
        armorTabSize.x = disableTabXSize;
        armorTabSize.y = disableTabYSize;
        armorRect.sizeDelta = armorTabSize;
        Vector2 consumeTabSize = consumeRect.sizeDelta;
        consumeTabSize.x = disableTabXSize;
        consumeTabSize.y = disableTabYSize;
        consumeRect.sizeDelta = consumeTabSize;
        
    }

    public void ArmorTabClick(PointerEventData data)
    {
        MoveArmorTab();
    }

    public void MoveArmorTab()
    {
        if (Managers.InvenUI.equipUI != null)
        {
            Managers.UI.ClosePopupUI(Managers.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = activeTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = disabledTab;
        for (int i = 0; i < Managers.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = Managers.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Equipment && (invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.Body || invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.Head))
            {
                Managers.UI.ShowUI(invenItem);
            }
            else
            {
                Managers.UI.HideUI(invenItem);
            }
        }

        Vector2 weaponTabSize = weaponRect.sizeDelta;
        weaponTabSize.x = disableTabXSize;
        weaponTabSize.y = disableTabYSize;
        weaponRect.sizeDelta = weaponTabSize;
        Vector2 armorTabSize = armorRect.sizeDelta;
        armorTabSize.x = activeTabXSize;
        armorTabSize.y = activeTabYSize;
        armorRect.sizeDelta = armorTabSize;
        Vector2 consumeTabSize = consumeRect.sizeDelta;
        consumeTabSize.x = disableTabXSize;
        consumeTabSize.y = disableTabYSize;
        consumeRect.sizeDelta = consumeTabSize;
    }

    public void ConsumeTabClick(PointerEventData data)
    {
        MoveConsumeTab();
    }

    public void MoveConsumeTab()
    {
        if (Managers.InvenUI.equipUI != null)
        {
            Managers.UI.ClosePopupUI(Managers.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = activeTab;
        for (int i = 0; i < Managers.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = Managers.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Consume)
            {
                Managers.UI.ShowUI(invenItem);
            }
            else
            {
                Managers.UI.HideUI(invenItem);
            }
        }

        Vector2 weaponTabSize = weaponRect.sizeDelta;
        weaponTabSize.x = disableTabXSize;
        weaponTabSize.y = disableTabYSize;
        weaponRect.sizeDelta = weaponTabSize;
        Vector2 armorTabSize = armorRect.sizeDelta;
        armorTabSize.x = disableTabXSize;
        armorTabSize.y = disableTabYSize;
        armorRect.sizeDelta = armorTabSize;
        Vector2 consumeTabSize = consumeRect.sizeDelta;
        consumeTabSize.x = activeTabXSize;
        consumeTabSize.y = activeTabYSize;
        consumeRect.sizeDelta = consumeTabSize;
    }
}
