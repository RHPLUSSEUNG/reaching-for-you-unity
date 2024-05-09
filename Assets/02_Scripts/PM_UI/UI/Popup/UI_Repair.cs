using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Repair : UI_Popup
{
    enum repairUI
    {
        CharList,
        InvenPanel,
        CloseButton
    }
    GameObject charList;
    GameObject invenPanel;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(repairUI));

        charList = Get<GameObject>((int)repairUI.CharList);
        invenPanel = Get<GameObject>((int)repairUI.InvenPanel);
        GameObject closeBtn = Get<GameObject>((int)repairUI.CloseButton);

        BindEvent(closeBtn.gameObject, OnCloseButton, Define.UIEvent.Click);
    }

    public void OnCloseButton(PointerEventData data)
    {
        Debug.Log("Close");
        base.ClosePopUI();
    }
}
