using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipRepairUI : UI_Base
{
    enum EquipUI
    {
        ItemIcon,
        ItemName,
        ItemElement,
        ItemType
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(EquipUI));

        BindEvent(gameObject, ItemButtonClick, Define.UIEvent.Click);
    }

    public void SetInfo(Image icon)
    {
        // Equip ���� �ݿ�
        Image itemIcon = GetObject((int)EquipUI.ItemIcon).GetComponent<Image>();
        itemIcon.sprite = icon.sprite;
    }

    public void ItemButtonClick(PointerEventData data)
    {
        if (Managers.InvenUI.equipUI == null)
        {
            Managers.InvenUI.CreateEquipUI();
        }
        Managers.InvenUI.equipUI.SetUIPosition();
    }

}
