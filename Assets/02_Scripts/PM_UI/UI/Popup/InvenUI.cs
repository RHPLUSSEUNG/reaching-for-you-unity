using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenUI : UI_Popup
{
    enum invenUI
    {
        HeadEquip,
        BodyEquip,
        WeaponEquip,
        InvenContent,
        WeaponTab,
        ArmorTab,
        ConsumeTab,
        CloseButton
    }

    [SerializeField] Sprite activeTab;
    [SerializeField] Sprite disabledTab;
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(invenUI));

        PM_UI_Manager.InvenUI.head = GetObject((int)invenUI.HeadEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.body = GetObject((int)invenUI.BodyEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.weapon = GetObject((int)invenUI.WeaponEquip).GetComponent<Image>();
        PM_UI_Manager.InvenUI.invenContent = GetObject((int)invenUI.InvenContent);

        Transform content = GetObject((int)invenUI.InvenContent).transform;
        for (int i = 0; i < content.childCount; i++)
        {
            PM_UI_Manager.Resource.Destroy(content.GetChild(i).gameObject);
        }

        BindEvent(GetObject((int)invenUI.HeadEquip), HeadEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.BodyEquip), BodyEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.WeaponEquip), WeaponEquipButtonClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.WeaponTab), WeaponTabClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.ArmorTab), ArmorTabClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.ConsumeTab), ConsumeTabClick, Define.UIEvent.Click);
        BindEvent(GetObject((int)invenUI.CloseButton), CloseButtonClick, Define.UIEvent.Click);
    }

    public void HeadEquipButtonClick(PointerEventData data)
    {
        MoveArmorTab();
        PM_UI_Manager.InvenUI.part = EquipPart.Head;
        PM_UI_Manager.InvenUI.UnEquipInvenUI();
    }
    public void BodyEquipButtonClick(PointerEventData data)
    {
        MoveArmorTab();
        PM_UI_Manager.InvenUI.part = EquipPart.UpBody;
        PM_UI_Manager.InvenUI.UnEquipInvenUI();
    }

    public void WeaponEquipButtonClick(PointerEventData data)
    {
        MoveWeaponTab();
        PM_UI_Manager.InvenUI.part = EquipPart.Weapon;
        PM_UI_Manager.InvenUI.UnEquipInvenUI();
    }


    public void WeaponTabClick(PointerEventData data)
    {
        MoveWeaponTab();
    }
    public void MoveWeaponTab()
    {
        if (PM_UI_Manager.InvenUI.equipUI != null)
        {
            PM_UI_Manager.UI.ClosePopupUI(PM_UI_Manager.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = activeTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = disabledTab;
        for (int i = 0; i < PM_UI_Manager.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = PM_UI_Manager.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Equipment && invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.Weapon)
            {
                PM_UI_Manager.UI.ShowUI(invenItem);
            }
            else
            {
                PM_UI_Manager.UI.HideUI(invenItem);
            }
        }
    }

    public void ArmorTabClick(PointerEventData data)
    {
        MoveArmorTab();
    }

    public void MoveArmorTab()
    {
        if (PM_UI_Manager.InvenUI.equipUI != null)
        {
            PM_UI_Manager.UI.ClosePopupUI(PM_UI_Manager.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = activeTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = disabledTab;
        for (int i = 0; i < PM_UI_Manager.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = PM_UI_Manager.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Equipment && (invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.UpBody || invenItem.GetComponent<EquipItemUI>().equipPart == EquipPart.Head))
            {
                PM_UI_Manager.UI.ShowUI(invenItem);
            }
            else
            {
                PM_UI_Manager.UI.HideUI(invenItem);
            }
        }
    }

    public void ConsumeTabClick(PointerEventData data)
    {
        MoveConsumeTab();
    }

    public void MoveConsumeTab()
    {
        if (PM_UI_Manager.InvenUI.equipUI != null)
        {
            PM_UI_Manager.UI.ClosePopupUI(PM_UI_Manager.InvenUI.equipUI);
        }
        GetObject((int)invenUI.WeaponTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ArmorTab).GetComponent<Image>().sprite = disabledTab;
        GetObject((int)invenUI.ConsumeTab).GetComponent<Image>().sprite = activeTab;
        for (int i = 0; i < PM_UI_Manager.InvenUI.invenContent.transform.childCount; i++)
        {
            GameObject invenItem = PM_UI_Manager.InvenUI.invenContent.transform.GetChild(i).gameObject;
            if (invenItem.GetComponent<InvenItemUI>().itemType == ItemType.Consume)
            {
                PM_UI_Manager.UI.ShowUI(invenItem);
            }
            else
            {
                PM_UI_Manager.UI.HideUI(invenItem);
            }
        }
    }

    public void CloseButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
