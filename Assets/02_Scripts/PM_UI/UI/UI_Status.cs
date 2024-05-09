using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Status : UI_Base
{
    enum GameObjects
    {
        StatusIcon,

    }
    GameObject status;
    int Max_Display_Child = 3;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GameObject statusIcon = GetObject((int)GameObjects.StatusIcon).gameObject;
        // BindEvent(statusIcon, OnStatusEnter, Define.UIEvent.Enter);
        BindEvent(statusIcon, OnStatusExit, Define.UIEvent.Exit);
        BindEvent(statusIcon, OnClick, Define.UIEvent.Click);
        status = gameObject;
        status.SetActive(false);
        Transform layout = status.transform.parent;
        for (int i = 0; i < Max_Display_Child; i++)
        {
            layout.GetChild(i).gameObject.SetActive(true);
        }

    }

    public void SetStatusImage(Image icon)
    {
        Image status_icon = Util.FindChild<Image>(status, "StatusIcon");
        status_icon.sprite = icon.sprite;
    }

    public void OnClick(PointerEventData data)
    {
        Debug.Log("Mouse Click");
        GameObject layout = status.transform.parent.gameObject;
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);
            childStatus.gameObject.SetActive(true);
        }
    }

    public void OnStatusEnter(PointerEventData data)
    {
        Debug.Log("Mouse Enter");
        GameObject layout = status.transform.parent.gameObject;
        for (int i = 1; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);
            childStatus.gameObject.SetActive(true);
        }
    }

    public void OnStatusExit(PointerEventData data)
    {
        Debug.Log("Mouse Enter");
        GameObject layout = status.transform.parent.gameObject;
        for (int i = Max_Display_Child; i < layout.transform.childCount; i++)
        {
            Transform childStatus = layout.transform.GetChild(i);
            childStatus.gameObject.SetActive(false);
        }
    }
}
