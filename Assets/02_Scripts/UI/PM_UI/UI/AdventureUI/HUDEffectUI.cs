using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDEffectUI : UI_Base
{
    enum StatusUI
    {
        StatusIcon,
        RemainText
    }
    public int Max_Display_Child = 3;
    GameObject statusIcon;
    TextMeshProUGUI remainText;
    public override void Init()
    {
        Bind<GameObject>(typeof(StatusUI));

        statusIcon = GetObject((int)StatusUI.StatusIcon).gameObject;
        remainText = GetObject((int)StatusUI.RemainText).gameObject.GetComponent<TextMeshProUGUI>();
        // BindEvent(statusIcon, StatusEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, StatusExit, Define.UIEvent.Exit);
        BindEvent(gameObject, StatusClick, Define.UIEvent.Click);
    }

    public void SetStatusImage(Sprite icon, int value = 1)
    {
        Image statusImage = statusIcon.GetComponent<Image>();
        statusImage.sprite = icon;
        remainText.text = value.ToString();
        if (value == 1)
        {
            remainText.gameObject.SetActive(false);
        }
        if (value == 0)
        {
            gameObject.SetActive(false);
            // transform.SetSiblingIndex(transform.parent.childCount - 1);
        }
    }

    public void StatusClick(PointerEventData data)
    {
        GameObject layout = gameObject.transform.parent.gameObject;
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);

            childStatus.gameObject.SetActive(true);
        }
    }

    public void StatusEnter(PointerEventData data)
    {
        GameObject layout = gameObject.transform.parent.gameObject;
        for (int i = 1; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);
            string valueText = childStatus.GetComponent<HUDEffectUI>().remainText.text;
            Debug.Log($"valueText : {valueText}");
            if(valueText.Equals("0"))
            {
                childStatus.gameObject.SetActive(false);
                break;
            }
            childStatus.gameObject.SetActive(true);
        }
    }

    public void StatusExit(PointerEventData data)
    {
        GameObject layout = gameObject.transform.parent.gameObject;
        for (int i = Max_Display_Child; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);
            childStatus.gameObject.SetActive(false);
        }
    }
}
