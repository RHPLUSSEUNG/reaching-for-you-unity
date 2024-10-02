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

    public void SetItemInfo()
    {
        EquipmentData itemData = Managers.Data.GetItemData(invenItemID, true) as EquipmentData;
        // 아이템의 정보로 변경
        itemType = itemData.ItemType;
        if (itemType == ItemType.Equipment)
        {
            switch(itemData.part)
            {
                case EquipPart.Head:
                    equipPart = EquipPart.Head;
                    break;
                case EquipPart.Body:
                    equipPart = EquipPart.Body;
                    break;
                case EquipPart.Weapon:
                    equipPart = EquipPart.Weapon;
                    break;
            }
        }
        
        itemIcon = GetObject((int)invenItemUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = itemData.itemSprite;
        Text itemName = GetObject((int)invenItemUI.ItemName).GetComponent<Text>();
        itemName.text = itemData.ItemName;
        
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
