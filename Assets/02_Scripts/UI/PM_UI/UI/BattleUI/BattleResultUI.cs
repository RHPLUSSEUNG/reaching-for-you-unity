using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleResultUI : UI_Popup
{
    enum battleResultUI
    {
        CharacterLayout,
        AcqItemContent,
        ExitButton
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(battleResultUI));

        GameObject exitBtn = GetObject((int)battleResultUI.ExitButton);
        BindEvent(exitBtn, OnClickExitButton, Define.UIEvent.Click);
    }

    public void OnClickExitButton(PointerEventData eventData)
    {
        Debug.Log("Click Exit Button");
        // 씬 전환 코드
    }
}
