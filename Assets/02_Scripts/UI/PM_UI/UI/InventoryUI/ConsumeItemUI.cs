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
    public override void Init()
    {
        Bind<GameObject>(typeof(consumeItemUI));
        BindEvent(gameObject, ClickConsumeItem, Define.UIEvent.Click);
    }

    public void SetItemInfo(int value)
    {
        // 아이템의 정보로 변경
        ItemData itemData = Managers.Data.GetItemData(invenItemID);
        itemType = itemData.ItemType;

        itemIcon = GetObject((int)consumeItemUI.ItemIcon).GetComponent<Image>();
        itemCount = GetObject((int)consumeItemUI.ItemCount).GetComponent<Text>();
        itemCount.text = value.ToString();
        Text itemName = GetObject((int)consumeItemUI.ItemName).GetComponent<Text>();
        itemName.text = itemData.ItemName;
        itemIcon.sprite = itemData.itemSprite;
    }
    
    public void UpdateConsumeValue(int value)
    {
        if(value == 0)
        {
            Managers.Prefab.Destroy(gameObject);
            return;
        }
        itemCount.text = value.ToString();
    }

    public void ClickConsumeItem(PointerEventData data)
    {
        Managers.InvenUI.focusItem = gameObject;
        Managers.InvenUI.focusItemID = invenItemID;
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
