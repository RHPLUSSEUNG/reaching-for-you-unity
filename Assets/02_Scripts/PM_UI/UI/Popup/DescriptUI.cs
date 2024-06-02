using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptUI : UI_Popup
{
    enum descriptUI
    {
        DescriptName,
        DescriptIcon,
        DescriptLayout,
        DescriptText
    }

    RectTransform descriptPanel;
    RectTransform descriptLayout;
    RectTransform descriptText;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(descriptUI));
        descriptPanel = gameObject.GetComponent<RectTransform>();
        descriptLayout = GetObject((int)descriptUI.DescriptLayout).GetComponent<RectTransform>();
        descriptText = GetObject((int)descriptUI.DescriptText).GetComponent<RectTransform>();

    }

    public void SetDescript(GameObject info, string test)
    {
        Text systemName = GetObject((int)descriptUI.DescriptName).GetComponent<Text>();
        Image systemIcon = GetObject((int)descriptUI.DescriptIcon).GetComponent<Image>();
        Text systemText = GetObject((int)descriptUI.DescriptText).GetComponent<Text>();
        systemText.text = test;
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptText);
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptLayout);
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptPanel);
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
