using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActUI : UI_Popup
{
    enum actUI
    {
        ActPanel,
        MagicPanel,
        ItemPanel,
        MagicUseButton,
        ItemUseButton,
        DefenseButton,
        NextTurnButton,
        MagicButtonLayout,
        ItemLayout,
        DescriptUI,
        MoveButton
    }

    GameObject actPanel;
    GameObject magicPanel;
    GameObject itemPanel;
    GameObject magicBtnLayout;
    GameObject descriptPanel;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(actUI));

        Managers.BattleUI.actUI = gameObject.GetComponent<ActUI>();
        GameObject magicUseBtn = GetObject((int)actUI.MagicUseButton);
        GameObject itemUseBtn = GetObject((int)actUI.ItemUseButton);
        GameObject defenseBtn = GetObject((int)actUI.DefenseButton);
        GameObject nextBtn = GetObject((int)actUI.NextTurnButton);
        GameObject moveBtn = GetObject((int)actUI.MoveButton);
        actPanel = GetObject((int)actUI.ActPanel);
        magicPanel = GetObject((int)actUI.MagicPanel);
        Managers.BattleUI.magicPanel = magicPanel;
        itemPanel = GetObject((int)actUI.ItemPanel);
        Managers.BattleUI.itemPanel = itemPanel;
        magicBtnLayout = GetObject((int)actUI.MagicButtonLayout);
        descriptPanel = GetObject((int)actUI.DescriptUI);
        Managers.BattleUI.descriptPanel = descriptPanel;

        BindEvent(magicUseBtn, UseMagicButtonClick, Define.UIEvent.Click);
        BindEvent(itemUseBtn, UseItemButtonClick, Define.UIEvent.Click);
        BindEvent(defenseBtn, UseDefenseButtonClick, Define.UIEvent.Click);
        BindEvent(nextBtn, NextButtonClick, Define.UIEvent.Click);
        BindEvent(moveBtn, MoveButtonClick, Define.UIEvent.Click);

        Managers.UI.HideUI(itemPanel);
        Managers.UI.HideUI(descriptPanel);
    }

    public void UpdateCharacterInfo()
    {
        Managers.UI.ShowUI(gameObject);
        // �÷��̾� �ϸ��� ����
        SkillList skillList = Managers.Battle.currentCharacter.GetComponent<SkillList>();
        // int curMp = Managers.Battle.currentCharacter.GetComponent<EntityStat>().Mp;
        for (int i = 0; i < skillList.list.Count; i++)
        {
            MagicButtonUI magicBtn = magicBtnLayout.transform.GetChild(i).GetComponent<MagicButtonUI>();
            if(magicBtn.SaveSkill == null)
            {
                magicBtn.SetSkill(skillList.list[i]);
            }
            // magicBtn.CheckEnableMagic(curMp);
        }
        if (magicBtnLayout.transform.childCount < skillList.list.Count)
        {
            for (int i = skillList.list.Count; i < magicBtnLayout.transform.childCount; i++)
            {
                magicBtnLayout.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        Managers.UI.ShowUI(magicPanel);
    }

    public void UseMagicButtonClick(PointerEventData data)
    {
        Managers.UI.ShowUI(magicPanel);
        Managers.UI.HideUI(itemPanel);
    }

    public void UseItemButtonClick(PointerEventData data)
    {
        Managers.UI.ShowUI(itemPanel);
        Managers.UI.HideUI(magicPanel);
    }

    public void UseDefenseButtonClick(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
    }

    public void NextButtonClick(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
        // Managers.Battle.NextTurn();
    }

    public void MoveButtonClick(PointerEventData data)
    {
        Text moveText = GetObject((int)actUI.MoveButton).transform.GetChild(0).gameObject.GetComponent<Text>();
        if (Managers.UI.uiState == UIState.Idle)
        {
            Debug.Log("State Move");
            Managers.UI.uiState = UIState.Move;
            Managers.UI.HideUI(descriptPanel);
            Managers.UI.HideUI(actPanel);

            // Text ����(�ӽ�)
            moveText.text = "�̵� ����";
            return;
        }
        if (Managers.UI.uiState == UIState.Move)
        {
            Debug.Log("State Idle");
            Managers.UI.uiState = UIState.Idle;
            Managers.UI.ShowUI(actPanel);

            // Text ����(�ӽ�)
            moveText.text = "�̵�";
            return;
        }
        

    }
}
