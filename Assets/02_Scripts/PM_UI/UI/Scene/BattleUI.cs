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
        PM_UI_Manager.BattleUI.cancleBtn = magicCancleBtn;

        BindEvent(magicCancleBtn, MagicCancleButtonClick, Define.UIEvent.Click);

        PM_UI_Manager.UI.HideUI(magicCancleBtn);
    }

    public void MagicCancleButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.uiState = UIManager.UIState.Idle;
        PM_UI_Manager.BattleUI.skill = null;
        PM_UI_Manager.UI.ShowUI(PM_UI_Manager.BattleUI.actUI);
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.cancleBtn);
    }
}
