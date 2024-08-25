using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeEquipUI : InvenItemUI
{
    enum consumeEquipUI
    {
        EquipIcon,
        ConsumeCount
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(consumeEquipUI));

        BindEvent(gameObject, ClickConsumeEquipUI, Define.UIEvent.Click);
    }

    public void ClickConsumeEquipUI(PointerEventData data)
    {
        Managers.InvenUI.focusItemID = invenItemID;
        Managers.InvenUI.consume_equip_state = false;

        Image equipIcon = GetObject((int)consumeEquipUI.EquipIcon).GetComponent<Image>();
        equipIcon.sprite = null;
        TextMeshProUGUI countText = GetObject((int)consumeEquipUI.ConsumeCount).GetComponent<TextMeshProUGUI>();
        countText.text = "";
        // Managers.UI.CreatePopupUI<ConsumeCountUI>();
        Managers.InvenUI.UpdateInvenUI();
    }
}
