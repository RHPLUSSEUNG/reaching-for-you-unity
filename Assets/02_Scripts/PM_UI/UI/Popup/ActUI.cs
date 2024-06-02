using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActUI : UI_Popup
{
    enum actUI
    {
        MagicPanel,
        ItemPanel,
        MagicUseButton,
        ItemUseButton,
        DefenseButton,
        NextTurnButton,
        MagicButtonLayout,
        ItemLayout,
        DescriptPanel
    }

    GameObject magicPanel;
    GameObject itemPanel;
    GameObject magicBtnLayout;
    GameObject descriptPanel;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(actUI));

        PM_UI_Manager.BattleUI.actUI = gameObject;
        GameObject magicUseBtn = GetObject((int)actUI.MagicUseButton);
        GameObject itemUseBtn = GetObject((int)actUI.ItemUseButton);
        GameObject defenseBtn = GetObject((int)actUI.DefenseButton);
        GameObject nextBtn = GetObject((int)actUI.NextTurnButton);
        magicPanel = GetObject((int)actUI.MagicPanel);
        PM_UI_Manager.BattleUI.magicPanel = magicPanel;
        itemPanel = GetObject((int)actUI.ItemPanel);
        PM_UI_Manager.BattleUI.itemPanel = itemPanel;
        magicBtnLayout = GetObject((int)actUI.MagicButtonLayout);
        descriptPanel = GetObject((int)actUI.DescriptPanel);
        PM_UI_Manager.BattleUI.descriptPanel = descriptPanel;

        BindEvent(magicUseBtn, UseMagicButtonClick, Define.UIEvent.Click);
        BindEvent(itemUseBtn, UseItemButtonClick, Define.UIEvent.Click);
        BindEvent(defenseBtn, UseDefenseButtonClick, Define.UIEvent.Click);
        BindEvent(nextBtn, NextButtonClick, Define.UIEvent.Click);

        PM_UI_Manager.UI.HideUI(itemPanel);
        PM_UI_Manager.UI.HideUI(descriptPanel);
    }

    public void UpdateCharacterInfo()
    {
        // 플레이어 턴마다 실행
        SkillList skillList = Managers.Battle.currentCharacter.GetComponent<SkillList>();
        for (int i = 0; i < skillList.list.Count; i++)
        {
            MagicButtonUI magicBtn = magicBtnLayout.transform.GetChild(i).GetComponent<MagicButtonUI>();
            magicBtn.SetSkill(skillList.list[i]);
        }
        if (magicBtnLayout.transform.childCount < skillList.list.Count)
        {
            for (int i = skillList.list.Count; i < magicBtnLayout.transform.childCount; i++)
            {
                magicBtnLayout.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        PM_UI_Manager.UI.ShowUI(magicPanel);
    }

    public void UseMagicButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);
    }

    public void UseItemButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.ShowUI(itemPanel);
        PM_UI_Manager.UI.HideUI(magicPanel);
    }

    public void UseDefenseButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);
        PM_UI_Manager.UI.HideUI(gameObject);
    }

    public void NextButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(magicPanel);
        PM_UI_Manager.UI.HideUI(itemPanel);
        PM_UI_Manager.UI.HideUI(gameObject);
        // Managers.Battle.NextTurn();
    }
}
