using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        PM_UI_Manager.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopUI()
    {
        PM_UI_Manager.UI.ClosePopupUI(this);
    }
}
