using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Save : UI_Base
{
    enum SaveButton
    {
        CloseButton
    }

    Button closeBtn;
    public bool panelState = false;
    public override void Init()
    {
        Bind<Button>(typeof(SaveButton));

        closeBtn = GetButton((int)SaveButton.CloseButton);
        BindEvent(closeBtn.gameObject, PanelClose, Define.UIEvent.Click);

        gameObject.SetActive(panelState);
    }

    public void PanelClose(PointerEventData data)
    {
        panelState = !panelState;
        gameObject.SetActive(panelState);
    }
}
