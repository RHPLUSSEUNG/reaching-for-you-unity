using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipUI : UI_Popup
{
    enum equipUI
    {
        EquipPanel,
        EquipButton
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(equipUI));
        GameObject equipBtn = GetObject((int)equipUI.EquipButton);
        BindEvent(equipBtn, EquipButtonClick, Define.UIEvent.Click);
    }

    public void SetUIPosition()
    {
        GameObject equipPanel = GetObject((int)equipUI.EquipPanel);
        RectTransform uiPos = equipPanel.GetComponent<RectTransform>();
        Vector3 newPosition = Input.mousePosition + new Vector3(uiPos.rect.width, 0, 0);
        uiPos.position = newPosition;
    }
    public void EquipButtonClick(PointerEventData data)
    {
        Managers.InvenUI.UpdateInvenUI();
        Managers.UI.ClosePopupUI();
    }
}
