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
        if (invenItemID == -1)
        {
            Debug.Log("Empty Consume Item");
            return;
        }
        Managers.InvenUI.focusItemID = invenItemID;
        invenItemID = -1;
        Managers.InvenUI.consume_equip_state = false;

        Image equipIcon = GetObject((int)consumeEquipUI.EquipIcon).GetComponent<Image>();
        equipIcon.sprite = Managers.InvenUI.emptySprite;
        TextMeshProUGUI countText = GetObject((int)consumeEquipUI.ConsumeCount).GetComponent<TextMeshProUGUI>();
        countText.text = "";
        // Managers.UI.CreatePopupUI<ConsumeCountUI>();
        Managers.InvenUI.UpdateInvenUI();
    }
}
