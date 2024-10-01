using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldDetectUI : UI_Base
{
    enum worldDetectUI
    {
        DetectImage
    }

    Image detectImage;
    bool state;
    public override void Init()
    {
        Bind<Image>(typeof(worldDetectUI));

        detectImage = GetImage((int)worldDetectUI.DetectImage);
    }

    public Image GetDetectImage()
    {
        if (detectImage != null)
        {
            return detectImage;
        }
        Debug.Log("DetectImage Error");
        return null;
    }
    public bool GetState()
    {
        return state;
    }
}
