using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PreBattle : UI_Scene
{
    public GameObject SavePanel;
    public GameObject RepairPanel;
    enum PreBattleButtons
    {
        StartButton,
        BatchButton,
        RepairButton,
        SaveButton,
        SystemButton
    }

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
        RepairPanel.SetActive(true);
    }

    public void OnSaveButton(PointerEventData data)
    {
        SavePanel.GetComponent<UI_Save>().panelState = !SavePanel.GetComponent<UI_Save>().panelState;
        SavePanel.SetActive(SavePanel.GetComponent<UI_Save>().panelState);
    }

    public void OnSystemButton(PointerEventData data)
    {
        
    }
}
