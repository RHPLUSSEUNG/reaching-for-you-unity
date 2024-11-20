using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemExplainUI : UI_Popup
{
    enum itemExplainUI
    {
        ItemExplainPanel,
        ItemIcon,
        ItemName,
        ItemExplain,
        CloseButton
    }

    GameObject itemExplainPanel;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(itemExplainUI));

        itemExplainPanel = GetObject((int)itemExplainUI.ItemExplainPanel);
        SetPosition();

        GameObject closeBtn = GetObject((int)itemExplainUI.CloseButton);
        BindEvent(closeBtn, ClickCloseButton, Define.UIEvent.Click);

        RectTransform panelRect = GetObject((int)itemExplainUI.ItemExplainPanel).GetComponent<RectTransform>();
        StartCoroutine(AnimPopup(panelRect));
    }

    public void SetItemInfo()
    {
        ItemData itemData = (ItemData)Managers.Data.ParsingData(Managers.InvenUI.focusItemID);
        
        Image itemIcon = GetObject((int)itemExplainUI.ItemIcon).GetComponent<Image>();
        TextMeshProUGUI itemName = GetObject((int)itemExplainUI.ItemName).GetComponent<TextMeshProUGUI>();
        itemIcon.sprite = itemData.itemSprite;
        itemName.text = itemData.ItemName;
    }

    public void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        RectTransform uiTransform = itemExplainPanel.GetComponent<RectTransform>();

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float uiWidth = uiTransform.rect.width;
        float uiHeight = uiTransform.rect.height;

        Vector3 uiPos = mousePos;
        if (mousePos.x + uiWidth > screenWidth)
        {
            uiPos.x = mousePos.x - (uiWidth / 2);
        }
        else
        {
            uiPos.x = mousePos.x + (uiWidth / 2);
        }
        if (mousePos.y - uiHeight < 0)
        {
            uiPos.y = mousePos.y + (uiHeight / 2);
        }
        else
        {
            uiPos.y = mousePos.y;
        }

        uiTransform.position = uiPos;
    }

    public void ClickCloseButton(PointerEventData data)
    {
        Managers.Prefab.Destroy(gameObject);
    }
}
