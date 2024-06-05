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

    Text itemCount;
    int count = 1;
    public override void Init()
    {
        Bind<GameObject>(typeof(consumeItemUI));
        BindEvent(gameObject, ClickConsumeItem, Define.UIEvent.Click);

        itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        itemCount = GetObject((int)consumeItemUI.ItemCount).GetComponent<Text>();
        itemCount.text = count.ToString();
    }

    public void SetItemInfo(Image item)
    {
        // 아이템의 정보로 변경
        itemType = invenItem.GetComponent<Item>().type;

        itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = item.sprite;
    }
    
    public void IncreaseCountUI()
    {
        count++;
        itemCount.text = count.ToString();
    }

    public void DecreaseCountUI()
    {
        count--;
        if (count == 0)
        {
            PM_UI_Manager.Resource.Destroy(gameObject);
            return;
        }
        itemCount.text = count.ToString();
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
