using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItemUI : UI_Base
{
    public EquipPart equipPart = EquipPart.Weapon;
    enum invenItemUI
    {
        ItemIcon,
        ItemName,
        ElementIcon,
        TypeIcon,
        AttackText,
        SpeedText,
        FigureText,
        ExtraText
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(invenItemUI));
        BindEvent(gameObject, ClickInvenItem, Define.UIEvent.Click);
    }

    public void ClickInvenItem(PointerEventData data)
    {
        Image equipIcon;
        Image itemIcon = GetObject((int)invenItemUI.ItemIcon).GetComponent<Image>();
        
        if(PM_UI_Manager.InvenUI.equipUI == null)
        {
            PM_UI_Manager.InvenUI.CreateEquipUI();
        }
        PM_UI_Manager.InvenUI.equipUI.SetUIPosition();

        switch (equipPart)
        {
            case EquipPart.Head:
                equipIcon = Util.FindChild(PM_UI_Manager.InvenUI.head.gameObject, "EquipIcon").GetComponent<Image>();
                PM_UI_Manager.InvenUI.equipUI.cur = equipIcon;
                PM_UI_Manager.InvenUI.equipUI.change = itemIcon;
                break;
            case EquipPart.UpBody:
                equipIcon = Util.FindChild(PM_UI_Manager.InvenUI.upbody.gameObject, "EquipIcon").GetComponent<Image>();
                equipIcon.sprite = itemIcon.sprite;
                break;
            case EquipPart.DownBody:
                equipIcon = Util.FindChild(PM_UI_Manager.InvenUI.downbody.gameObject, "EquipIcon").GetComponent<Image>();
                equipIcon.sprite = itemIcon.sprite;
                break;
            case EquipPart.Weapon:
                equipIcon = Util.FindChild(PM_UI_Manager.InvenUI.weapon.gameObject, "EquipIcon").GetComponent<Image>();
                PM_UI_Manager.InvenUI.equipUI.cur = equipIcon;
                PM_UI_Manager.InvenUI.equipUI.change = itemIcon;  
                break;
        }
    }
}
