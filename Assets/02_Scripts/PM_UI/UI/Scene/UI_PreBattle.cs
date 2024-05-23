using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PreBattle : UI_Scene
{
    public GameObject SavePanel;
    enum PreBattleButtons
    {
        StartButton,
        BatchButton,
        RepairButton,
        SaveButton,
        SystemButton
    }

    UI_Repair repairUI = null;
    UI_Save saveUI = null;
    UI_Pause pauseUI = null;

    //Test
    public TestPlayerInfo[] testList = new TestPlayerInfo[4];       // test

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

    }

    public void OnStartButton(PointerEventData data)
    {
        Debug.Log("Game Start");
    }

    public void OnBatchButton(PointerEventData data)
    {

    }

    public void OnRepairButton(PointerEventData data)
    {
        if(repairUI == null)
        {
            repairUI = PM_UI_Manager.UI.CreatePopupUI<UI_Repair>("RepairUI");
        }
        // Test
        repairUI.tempPlayer[0] = testList[0];
        repairUI.tempPlayer[1] = testList[1];
        repairUI.tempPlayer[2] = testList[2];
        repairUI.tempPlayer[3] = testList[3];
        PM_UI_Manager.UI.ShowUI(repairUI.gameObject);
    }

    public void OnSaveButton(PointerEventData data)
    {
        if(saveUI == null)
        {
            saveUI = PM_UI_Manager.UI.CreatePopupUI<UI_Save>("SaveUI");
        }
        PM_UI_Manager.UI.ShowUI(saveUI.gameObject);
    }

    public void OnSystemButton(PointerEventData data)
    {
        if (pauseUI == null)
        {
            pauseUI = PM_UI_Manager.UI.CreatePopupUI<UI_Pause>("PauseUI");
        }
        PM_UI_Manager.UI.ShowUI(pauseUI.gameObject);
    }
}
