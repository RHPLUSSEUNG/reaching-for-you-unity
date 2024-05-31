using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicButtonUI : UI_Base
{
    enum magicButtonUI
    {
        MagicName,
        ElementIcon,
        ManaText
    }

    GameObject saveSkill;

    public override void Init()
    {
        Bind<GameObject>(typeof(magicButtonUI));
        BindEvent(gameObject, MagicButtonClick, Define.UIEvent.Click);
        BindEvent(gameObject, MagicButtonEnter, Define.UIEvent.Enter);
        BindEvent(gameObject, MagicButtonExit, Define.UIEvent.Exit);
    }

    public void SetSkill(GameObject skill)
    {
        Text magicName = GetObject((int)magicButtonUI.MagicName).GetComponent<Text>();
        // magicName.text;
        Image elementIcon = GetObject((int)magicButtonUI.ElementIcon).GetComponent<Image>();
        Text manaText = GetObject((int)magicButtonUI.ManaText).GetComponent<Text>();
        // 1. DataManager에 넣을거면 여기서 Manager를 통해 접근 -> RayCastManager에서 Manager를 통해 GetSkill
        // 2. 여기에 Skill을 저장하면 변수에 저장 -> 여기에 저장하면 GetSkill 함수를 파고 Find를 통해 또 찾아야함(현재 방식)
        // 3. 아니면 BattleUIManager를 하나 만들어보기

        skill = saveSkill;
    }

    public void MagicButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.uiState = UIManager.UIState.SkillSet;
        PM_UI_Manager.BattleUI.skill = saveSkill;
        Debug.Log("Button Click");
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.magicPanel);
        PM_UI_Manager.UI.HideUI(PM_UI_Manager.BattleUI.actUI);
    }

    public void MagicButtonEnter(PointerEventData data)
    {
        Debug.Log("Button Enter");
    }

    public void MagicButtonExit(PointerEventData data)
    {
        Debug.Log("Button Exit");
    }
}
