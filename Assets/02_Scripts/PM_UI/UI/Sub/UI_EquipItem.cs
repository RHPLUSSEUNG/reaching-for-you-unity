using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipItem : UI_Base
{
    enum EquipUI
    {
        ItemIcon,
        ItemName,
        ItemElement,
        ItemType,
        Attack,
        Speed
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(EquipUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
    }

    public void SetInfo(Equipment equip)
    {

    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("Item 버튼 클릭");
    }

}
