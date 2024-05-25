using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleUI));
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
