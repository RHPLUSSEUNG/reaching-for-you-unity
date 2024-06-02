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
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.itemPanel);
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.actUI);
    }

    public void ItemButtonEnter(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(PM_UI_Manager.BattleUI.descriptPanel);
        DescriptUI descript = PM_UI_Manager.BattleUI.descriptPanel.GetComponent<DescriptUI>();
        descript.SetPosition();
        descript.SetDescript(saveItem, "마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명마법에 대한 설명");
    }

    public void ItemButtonExit(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.descriptPanel);
    }
}
