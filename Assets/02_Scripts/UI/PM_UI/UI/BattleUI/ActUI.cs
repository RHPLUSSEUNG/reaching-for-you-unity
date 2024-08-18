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
        MoveButton,
        MagicCancleButton
    }

    GameObject mainCamera;
    CameraController cameraController;
    MoveRangeUI rangeUI;
    public SkillRangeUI skillRangeUI;       // Temp

    GameObject actPanel;
    GameObject magicPanel;
    GameObject itemPanel;
    GameObject magicBtnLayout;
    GameObject itemLayout;
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
        Managers.BattleUI.moveBtn = moveBtn;
        GameObject magicCancleBtn = GetObject((int)actUI.MagicCancleButton);
        Managers.BattleUI.cancleBtn = magicCancleBtn;
        actPanel = GetObject((int)actUI.ActPanel);
        Managers.BattleUI.actPanel = actPanel;
        magicPanel = GetObject((int)actUI.MagicPanel);
        Managers.BattleUI.magicPanel = magicPanel;
        itemPanel = GetObject((int)actUI.ItemPanel);
        Managers.BattleUI.itemPanel = itemPanel;
        magicBtnLayout = GetObject((int)actUI.MagicButtonLayout);
        itemLayout = GetObject((int)actUI.ItemLayout);
        descriptPanel = GetObject((int)actUI.DescriptUI);
        Managers.BattleUI.descriptPanel = descriptPanel;

        BindEvent(magicUseBtn, UseMagicButtonClick, Define.UIEvent.Click);
        BindEvent(itemUseBtn, UseItemButtonClick, Define.UIEvent.Click);
        BindEvent(defenseBtn, UseDefenseButtonClick, Define.UIEvent.Click);
        BindEvent(nextBtn, NextButtonClick, Define.UIEvent.Click);
        BindEvent(moveBtn, MoveEndButtonClick, Define.UIEvent.Click);
        BindEvent(magicCancleBtn, MagicCancleButtonClick, Define.UIEvent.Click);

        Managers.UI.HideUI(itemPanel);
        Managers.UI.HideUI(descriptPanel);
        Managers.UI.HideUI(magicCancleBtn);

        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();

        rangeUI = gameObject.GetComponent<MoveRangeUI>();
        rangeUI.SetMapInfo();
        skillRangeUI = gameObject.GetComponent<SkillRangeUI>();
        skillRangeUI.SetMapInfo();

        // Consume Test
        Managers.Party.FindPlayer("Player_Girl_Battle(Clone)").GetComponent<Equip_Item>().AddConsume(1, 3);
    }

    public void UpdateCharacterInfo()
    {
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.ShowUI(gameObject);

        SkillList skillList = Managers.Battle.currentCharacter.GetComponent<SkillList>();
        Equip_Item consumeList = Managers.Battle.currentCharacter.GetComponent<Equip_Item>();

        int curMp = Managers.Battle.currentCharacter.GetComponent<EntityStat>().Mp;
        for (int i = 0; i < skillList.list.Count; i++)
        {
            // MagicButtonUI magicButton = Managers.UI.MakeSubItem<MagicButtonUI>(magicBtnLayout.transform, "MagicButton");
            MagicButtonUI magicBtn = magicBtnLayout.transform.GetChild(i).GetComponent<MagicButtonUI>();
            if (magicBtn.SaveSkill == null)
            {
                magicBtn.SetSkill(skillList.list[i], skillList.idList[i]);
            }
            magicBtn.CheckEnableMagic(curMp);
        }
        if (magicBtnLayout.transform.childCount > skillList.list.Count)
        {
            for (int i = skillList.list.Count; i < magicBtnLayout.transform.childCount; i++)
            {
                magicBtnLayout.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        foreach (KeyValuePair<int, int> item in consumeList.Consumes)
        {
            BattleItemUI itemButton = Managers.UI.MakeSubItem<BattleItemUI>(itemLayout.transform, "ItemButton");
            // itemButton.SetItem(item.Key, item.Value);
        }

        Managers.UI.uiState = UIState.Move;
        Managers.BattleUI.PlayerMovePhaseUI();

        rangeUI.DisplayMoveRange();

        cameraController.ChangeCameraMode(CameraMode.Static, true, true);
        Managers.BattleUI.cameraMode = CameraMode.Static;
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
        cameraController.ChangeCameraMode(CameraMode.Static, true, true);
        Managers.BattleUI.cameraMode = CameraMode.Static;
        Managers.Battle.NextTurn();
    }

    public void NextButtonClick(PointerEventData data)
    {
        Managers.UI.HideUI(gameObject);
        cameraController.ChangeCameraMode(CameraMode.Static, true, true);
        Managers.BattleUI.cameraMode = CameraMode.Static;
        Managers.Battle.NextTurn();
    }

    public void MoveEndButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIState.Idle;
        Managers.BattleUI.PlayerBattlePhaseUI();

        rangeUI.ClearMoveRange();
        cameraController.ChangeOffSet(1, 2, -3, 30);   // 캐릭터 행동 UI offset
        cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.BattleUI.cameraMode = CameraMode.Follow;
    }

    public void MagicCancleButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIState.Idle;
        Managers.BattleUI.skill = null;
        Managers.BattleUI.PlayerBattlePhaseUI();

        cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        Managers.BattleUI.cameraMode = CameraMode.Follow;
        skillRangeUI.ClearSkillRange();
    }
}
