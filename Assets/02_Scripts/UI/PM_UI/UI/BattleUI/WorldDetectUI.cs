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
        detectImage.gameObject.SetActive(false);
    }

    public bool ShowDetectImage()
    {
        detectImage.gameObject.SetActive(true);
        state = true;
        return state;
    }

    public bool HideDetectImage()
    {
        detectImage.gameObject.SetActive(false);
        state = false;
        return state;
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
