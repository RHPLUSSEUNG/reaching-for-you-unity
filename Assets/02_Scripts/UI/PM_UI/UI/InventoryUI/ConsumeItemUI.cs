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
            Managers.Prefab.Destroy(gameObject);
            return;
        }
        itemCount.text = count.ToString();
    }

    public void ClickConsumeItem(PointerEventData data)
    {
        Managers.InvenUI.focusItem = gameObject;
        Managers.InvenUI.type = itemType;
        Image itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        Managers.InvenUI.changeIcon = itemIcon;

        if (Managers.InvenUI.equipUI == null)
        {
            Managers.InvenUI.CreateEquipUI();
        }
        Managers.InvenUI.equipUI.SetUIPosition();
    }
}
