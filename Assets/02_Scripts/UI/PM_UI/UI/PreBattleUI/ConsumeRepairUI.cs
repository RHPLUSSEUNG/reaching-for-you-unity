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
        Image itemIcon = GetObject((int)ItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = icon.sprite;
        Text itemCount = GetObject((int)ItemUI.ItemCount).GetComponent<Text>();
        itemCount.text = count.ToString();
    }

    public void ItemButtonClick(PointerEventData data)
    {
        Debug.Log("버튼 클릭");
    }
}
