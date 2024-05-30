using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipRepairUI : UI_Base
{
    enum EquipUI
    {
        ItemIcon,
        ItemName,
        ItemElement,
        ItemType
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(EquipUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
    }

    public void SetInfo(Image icon)
    {
        // Equip 정보 반영
        Image itemIcon = GetObject((int)EquipUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = icon.sprite;
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("Item 버튼 클릭");
    }

}
