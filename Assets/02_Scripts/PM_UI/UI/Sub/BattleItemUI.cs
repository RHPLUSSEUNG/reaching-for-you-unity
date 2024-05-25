using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleItemUI : UI_Base
{
    enum battleItemUI
    {
        
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(battleItemUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
        BindEvent(gameObject, ItemButtonEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, ItemButtonExit, Define.UIEvent.Exit);
    }

    public void SetItem()
    {
        
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("Item Button Click");
    }

    public void ItemButtonEnter(PointerEventData data)
    {
        Debug.Log("Item Button Enter");
    }

    public void ItemButtonExit(PointerEventData data)
    {
        Debug.Log("Item Button Exit");
    }
}
