using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleInfoUI : UI_Popup
{
    public override void Init()
    {
        base.Init();
    }
    public abstract void SetInfo(GameObject obj);

    public abstract void SetPosition();
}
