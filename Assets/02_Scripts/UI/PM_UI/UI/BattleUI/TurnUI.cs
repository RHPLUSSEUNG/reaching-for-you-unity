using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : UI_Base
{
    enum turnUI
    {
        TurnFrame,
        TurnImage
    }

    Image turnImage;
    public override void Init()
    {
        Bind<GameObject>(typeof(turnUI));
        turnImage = GetObject((int)turnUI.TurnImage).GetComponent<Image>();
    }

    public Image GetTurnImage()
    {
        if(turnImage == null)
        {
            return null;
        }
        return turnImage;
    }
}
