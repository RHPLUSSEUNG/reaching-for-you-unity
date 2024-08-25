using UnityEngine;
using UnityEngine.EventSystems;

public class EquipUI : UI_Popup
{
    enum equipUI
    {
        EquipPanel,
        EquipButton,
        ExplainButton
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(equipUI));
        GameObject equipBtn = GetObject((int)equipUI.EquipButton);
        BindEvent(equipBtn, EquipButtonClick, Define.UIEvent.Click);
        GameObject explainBtn = GetObject((int)equipUI.ExplainButton);
        BindEvent(explainBtn, ExplainButtonClick, Define.UIEvent.Click);
    }

    public void SetUIPosition()
    {
        GameObject equipPanel = GetObject((int)equipUI.EquipPanel);
        RectTransform uiPos = equipPanel.GetComponent<RectTransform>();
        Vector3 newPosition = Input.mousePosition + new Vector3(uiPos.rect.width, 0, 0);
        uiPos.position = newPosition;
    }
    public void EquipButtonClick(PointerEventData data)
    {
        if(Managers.InvenUI.type == ItemType.Consume)
        {
            Managers.InvenUI.consume_equip_state = true;
            Managers.UI.CreatePopupUI<ConsumeCountUI>("ConsumeCountUI");
            Managers.Prefab.Destroy(gameObject);
            return;
        }
        Managers.InvenUI.UpdateInvenUI();
        Managers.UI.ClosePopupUI();
    }
    public void ExplainButtonClick(PointerEventData data)
    {
        ItemExplainUI explainUI = Managers.UI.CreatePopupUI<ItemExplainUI>("ItemExplainUI");
        explainUI.SetItemInfo();
        Managers.Prefab.Destroy(gameObject);
    }
}
