using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Save : UI_Popup
{
    enum SaveButton
    {
        CloseButton
    }

    Button closeBtn;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(SaveButton));

        closeBtn = GetButton((int)SaveButton.CloseButton);
        BindEvent(closeBtn.gameObject, PanelClose, Define.UIEvent.Click);
    }

    public void PanelClose(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
    }
}
