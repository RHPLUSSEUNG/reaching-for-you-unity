using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    }

}
