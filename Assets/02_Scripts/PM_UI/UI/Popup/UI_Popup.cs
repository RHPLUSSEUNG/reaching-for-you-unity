using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        PM_UI_Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopUI()
    {
        PM_UI_Managers.UI.ClosePopupUI(this);
    }
}
