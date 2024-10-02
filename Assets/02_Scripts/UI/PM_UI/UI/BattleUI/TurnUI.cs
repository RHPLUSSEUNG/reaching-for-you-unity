using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUI : UI_Base
{
    enum turnUI
    {
        TurnImage
    }

    RectTransform turnFrameRect;
    RectTransform turnImageRect;
    public override void Init()
    {
        Bind<GameObject>(typeof(turnUI));
        turnFrameRect = GetComponent<RectTransform>();
        turnImageRect = GetObject((int)turnUI.TurnImage).GetComponent<RectTransform>();

        FitTurnImage();
    }

    void FitTurnImage()
    {
        if (turnImageRect != null && turnFrameRect != null)
        {
            float parentWidth = turnFrameRect.rect.width;
            float parentHeight = turnFrameRect.rect.height;

            float imageWidth = turnImageRect.rect.width;
            float imageHeight = turnImageRect.rect.height;
            
            float scaleFactor = Mathf.Min(parentWidth / imageWidth, parentHeight / imageHeight);

            turnImageRect.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        }
    }
}
