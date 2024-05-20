using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Status : UI_Base
{
    enum StatusUI
    {
        StatusIcon,

    }
    public int Max_Display_Child = 3;
    GameObject statusIcon;
    public override void Init()
    {
        Bind<GameObject>(typeof(StatusUI));

        statusIcon = GetObject((int)StatusUI.StatusIcon).gameObject;
        // BindEvent(statusIcon, StatusEnter, Define.UIEvent.Enter);
        BindEvent(statusIcon, StatusExit, Define.UIEvent.Exit);
        BindEvent(statusIcon, StatusClick, Define.UIEvent.Click);
    }

    public void SetStatusImage(Image icon)
    {
        Image statusImage = statusIcon.GetComponent<Image>();
        statusImage.sprite = icon.sprite;
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
