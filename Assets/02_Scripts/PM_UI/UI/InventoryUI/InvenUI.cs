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

        Managers.InvenUI.head = GetObject((int)invenUI.HeadEquip).GetComponent<Image>();
        Managers.InvenUI.body = GetObject((int)invenUI.BodyEquip).GetComponent<Image>();
        Managers.InvenUI.weapon = GetObject((int)invenUI.WeaponEquip).GetComponent<Image>();
        Managers.InvenUI.invenContent = GetObject((int)invenUI.InvenContent);

        Transform content = GetObject((int)invenUI.InvenContent).transform;
        for (int i = 0; i < content.childCount; i++)
        {
            Managers.Prefab.Destroy(content.GetChild(i).gameObject);
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
    }

    public void CloseButtonClick(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
    }
}
