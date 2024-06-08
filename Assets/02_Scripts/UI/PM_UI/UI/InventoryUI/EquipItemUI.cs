using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItemUI : InvenItemUI
{
    public EquipPart equipPart;
    enum invenItemUI
    {
        ItemIcon,
        ItemName,
        ElementIcon,
        TypeIcon,
        AttackText,
        SpeedText,
        FigureText,
        ExtraText
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(invenItemUI));
        BindEvent(gameObject, ClickInvenItem, Define.UIEvent.Click);

        itemIcon = GetObject((int)invenItemUI.ItemIcon).GetComponent<Image>();
    }

    public void SetItemInfo(Image item)
    {
        // 아이템의 정보로 변경
        itemType = invenItem.GetComponent<Item>().type;
        if (itemType == ItemType.Equipment)
        {
            equipPart = invenItem.GetComponent<Equipment>().part;
        }
        
        itemIcon = GetObject((int)invenItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = item.sprite;
    }

    public void ClickInvenItem(PointerEventData data)
    {
        Managers.InvenUI.focusItem = gameObject;
        Managers.InvenUI.type = itemType;
        Managers.InvenUI.part = equipPart;
        Image itemIcon = GetObject((int)invenItemUI.ItemIcon).GetComponent<Image>();
        Managers.InvenUI.changeIcon = itemIcon;

        if (Managers.InvenUI.equipUI == null)
        {
            Managers.InvenUI.CreateEquipUI();
        }
        Managers.InvenUI.equipUI.SetUIPosition();
    }
}
