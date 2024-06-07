using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PreBattle : UI_Scene
{
    enum PreBattleButtons
    {
        StartButton,
        BatchButton,
        RepairButton,
        SaveButton,
        SystemButton
    }

    BatchUI batchUI = null;
    UI_Repair repairUI = null;
    UI_Save saveUI = null;
    UI_Pause pauseUI = null;

    //Test
    public TestPlayerInfo[] testList = new TestPlayerInfo[4];
    public GameObject Test;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(PreBattleButtons));
        Util.GetOrAddComponent<UI_PreBattle>(gameObject);
        Button startBtn = GetButton((int)PreBattleButtons.StartButton);
        Button batchBtn = GetButton((int)PreBattleButtons.BatchButton);
        Button repairBtn = GetButton((int)PreBattleButtons.RepairButton);
        Button saveBtn = GetButton((int)PreBattleButtons.SaveButton);
        Button systemBtn = GetButton((int)PreBattleButtons.SystemButton);
        BindEvent(startBtn.gameObject, OnStartButton, Define.UIEvent.Click);
        BindEvent(batchBtn.gameObject, OnBatchButton, Define.UIEvent.Click);
        BindEvent(repairBtn.gameObject, OnRepairButton, Define.UIEvent.Click);
        BindEvent(saveBtn.gameObject, OnSaveButton, Define.UIEvent.Click);
        BindEvent(systemBtn.gameObject, OnSystemButton, Define.UIEvent.Click);

        // Test
        for(int i = 0; i < Test.transform.childCount; i++)
        {
            Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);
        }
    }

    public void OnStartButton(PointerEventData data)
    {
        // Managers.Battle.BattleStart();
        Managers.UI.CreateSceneUI<BattleUI>("BattleUI");
        ActUI actUI = Managers.UI.CreatePopupUI<ActUI>("ActUI");
        // PM_UI_Manager.UI.HideUI(actUI.gameObject);
        if (batchUI != null)
        {
            Managers.UI.ClosePopupUI(batchUI);
        }
        if (repairUI != null)
        {
            Managers.UI.ClosePopupUI(repairUI);
        }
        if (saveUI != null)
        {
            Managers.UI.ClosePopupUI(saveUI);
        }
        Managers.Prefab.Destroy(gameObject);
    }

    public void OnBatchButton(PointerEventData data)
    {
        if(batchUI == null)
        {
            batchUI = Managers.UI.CreatePopupUI<BatchUI>("BatchUI");
        }
        batchUI.preBattleUI = gameObject;
        Managers.UI.ShowUI(batchUI.gameObject);
        Managers.UI.HideUI(gameObject);
    }

    public void OnRepairButton(PointerEventData data)
    {
        if(repairUI == null)
        {
            repairUI = Managers.UI.CreatePopupUI<UI_Repair>("RepairUI");
        }
        // Test
        repairUI.tempPlayer[0] = testList[0];
        repairUI.tempPlayer[1] = testList[1];
        repairUI.tempPlayer[2] = testList[2];
        repairUI.tempPlayer[3] = testList[3];
        Managers.UI.ShowUI(repairUI.gameObject);
        repairUI.SetPlayerInfo();
        repairUI.SetInventory();
    }

    public void OnSaveButton(PointerEventData data)
    {
        if(saveUI == null)
        {
            saveUI = Managers.UI.CreatePopupUI<UI_Save>("SaveUI");
        }
        Managers.UI.ShowUI(saveUI.gameObject);
    }

    public void OnSystemButton(PointerEventData data)
    {
        if (pauseUI == null)
        {
            pauseUI = Managers.UI.CreatePopupUI<UI_Pause>("PauseUI");
        }
        Managers.UI.ShowUI(pauseUI.gameObject);
    }
}
