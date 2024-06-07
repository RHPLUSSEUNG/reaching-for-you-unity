using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleUI : UI_Scene
{
    enum battleUI
    {
        ActTurn,
        MagicCancleButton
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(battleUI));
        GameObject magicCancleBtn = GetObject((int)battleUI.MagicCancleButton);
        Managers.BattleUI.cancleBtn = magicCancleBtn;

        BindEvent(magicCancleBtn, MagicCancleButtonClick, Define.UIEvent.Click);

        Managers.UI.HideUI(magicCancleBtn);
    }

    public void MagicCancleButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIManager.UIState.Idle;
        Managers.BattleUI.skill = null;
        Managers.UI.ShowUI(Managers.BattleUI.actUI);
        Managers.UI.HideUI(Managers.BattleUI.cancleBtn);
    }
}
