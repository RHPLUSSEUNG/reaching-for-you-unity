using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleItemUI : UI_Base
{
    enum battleItemUI
    {
        ItemIcon,
        ItemName,
        ItemCount
    }

    GameObject saveItem;
    public override void Init()
    {
        Bind<GameObject>(typeof(battleItemUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
        BindEvent(gameObject, ItemButtonEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, ItemButtonExit, Define.UIEvent.Exit);
    }

    public void SetItem()
    {
        // PM_UI_Manager.BattleUI.item
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("Item Button Click");
        Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
        Managers.UI.HideUI(Managers.BattleUI.itemPanel);
        Managers.UI.HideUI(Managers.BattleUI.actUI.gameObject);
    }

    public void ItemButtonEnter(PointerEventData data)
    {
        Managers.UI.ShowUI(Managers.BattleUI.descriptPanel);
        DescriptUI descript = Managers.BattleUI.descriptPanel.GetComponent<DescriptUI>();
        descript.SetDescript(saveItem, "마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명");
        descript.SetPosition();
    }

    public void ItemButtonExit(PointerEventData data)
    {
        Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
    }
}
