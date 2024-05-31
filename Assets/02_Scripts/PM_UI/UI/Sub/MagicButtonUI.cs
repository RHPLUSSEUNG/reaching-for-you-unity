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
        // 1. DataManager�� �����Ÿ� ���⼭ Manager�� ���� ���� -> RayCastManager���� Manager�� ���� GetSkill
        // 2. ���⿡ Skill�� �����ϸ� ������ ���� -> ���⿡ �����ϸ� GetSkill �Լ��� �İ� Find�� ���� �� ã�ƾ���(���� ���)
        // 3. �ƴϸ� BattleUIManager�� �ϳ� ������

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
