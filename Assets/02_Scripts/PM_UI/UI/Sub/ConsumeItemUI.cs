using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeItemUI : InvenItemUI
{
    enum consumeItemUI
    {
        ItemIcon,
        ItemName,
        ItemDescript,
        ItemCount
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(consumeItemUI));
        BindEvent(gameObject, ClickConsumeItem, Define.UIEvent.Click);

        itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
    }

    public override void SetItemInfo(Image item)
    {
        // 아이템의 정보로 변경
        itemType = invenItem.GetComponent<Item>().type;

        itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = item.sprite;
    }

    public void ClickConsumeItem(PointerEventData data)
    {
        PM_UI_Manager.InvenUI.focusItem = gameObject;
        PM_UI_Manager.InvenUI.type = itemType;
        Image itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        PM_UI_Manager.InvenUI.changeIcon = itemIcon;

        if (PM_UI_Manager.InvenUI.equipUI == null)
        {
            PM_UI_Manager.InvenUI.CreateEquipUI();
        }
        PM_UI_Manager.InvenUI.equipUI.SetUIPosition();
    }
}
