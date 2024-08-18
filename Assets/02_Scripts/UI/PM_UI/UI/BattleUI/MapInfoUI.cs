using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapInfoUI : UI_Popup
{
    enum mapInfoUI
    {
        TileName,
        TileIcon,
        TileText
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(mapInfoUI));
    }

    public void SetInfo(GameObject tile)
    {
        Image icon = GetObject((int)mapInfoUI.TileIcon).GetComponent<Image>();
        TextMeshPro tileName = GetObject((int)mapInfoUI.TileName).GetComponent<TextMeshPro>();
        TextMeshPro tileText = GetObject((int)mapInfoUI.TileText).GetComponent<TextMeshPro>();
    }

    public void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        RectTransform uiTransform = gameObject.GetComponent<RectTransform>();

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
}
