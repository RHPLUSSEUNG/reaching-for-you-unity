using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void SetInfo(Equipment equip)
    {
        // Equip ���� �ݿ�
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("Item ��ư Ŭ��");
    }

}
