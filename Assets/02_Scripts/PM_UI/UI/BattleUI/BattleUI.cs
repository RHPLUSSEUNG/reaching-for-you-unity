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
    }

}
