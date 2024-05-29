using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeRepairUI : UI_Base
{
    enum ItemUI
    {
        ItemIcon,
        ItemCount
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(ItemUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
    }

    public void SetInfo(Image icon, int count)
    {
        GameObject item = Get<GameObject>((int)ItemUI.ItemIcon);
        Image itemIcon = item.GetComponent<Image>();
        itemIcon.sprite = icon.sprite;
        Text itemCount = item.GetComponent<Text>();
        itemCount.text = count.ToString();
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("버튼 클릭");
    }
}
