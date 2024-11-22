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
        SystemButton,
        MoveCameraButton
    }

    BatchUI batchUI = null;
    UI_Repair repairUI = null;
    UI_Save saveUI = null;
    UI_Pause pauseUI = null;
    CameraController cameraController;

    public Button repairBtn;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(PreBattleButtons));
        Util.GetOrAddComponent<UI_PreBattle>(gameObject);
        Button startBtn = GetButton((int)PreBattleButtons.StartButton);
        Button batchBtn = GetButton((int)PreBattleButtons.BatchButton);
        repairBtn = GetButton((int)PreBattleButtons.RepairButton);
        Button saveBtn = GetButton((int)PreBattleButtons.SaveButton);
        Button systemBtn = GetButton((int)PreBattleButtons.SystemButton);
        Button cameraBtn = GetButton((int)PreBattleButtons.MoveCameraButton);
        BindEvent(startBtn.gameObject, OnStartButton, Define.UIEvent.Click);
        BindEvent(batchBtn.gameObject, OnBatchButton, Define.UIEvent.Click);
        BindEvent(repairBtn.gameObject, OnRepairButton, Define.UIEvent.Click);
        BindEvent(saveBtn.gameObject, OnSaveButton, Define.UIEvent.Click);
        BindEvent(systemBtn.gameObject, OnSystemButton, Define.UIEvent.Click);
        // BindEvent(cameraBtn.gameObject, OnCameraButton, Define.UIEvent.Click);

        Managers.UI.HideUI(cameraBtn.gameObject);
        repairBtn.gameObject.SetActive(false);

        CameraController cameraController = Camera.main.GetComponent<CameraController>();

        Managers.BattleUI.warningUI = Managers.UI.CreatePopupUI<WarningUI>("WarningUI");
        Managers.UI.HideUI(Managers.BattleUI.warningUI.gameObject);
    }

    public void OnStartButton(PointerEventData data)
    {
        if(Managers.Battle.playerLive == 0)
        {
            Managers.BattleUI.warningUI.SetText("현재 맵에 플레이어가 존재하지 않습니다.");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return;
        }
        Managers.UI.CreateSceneUI<BattleUI>("BattleUI");
        ActUI actUI = Managers.UI.CreatePopupUI<ActUI>("ActUI");
        Managers.BattleUI.actUI = actUI;
        UI_ActTurn turnUI = Managers.UI.CreateSceneUI<UI_ActTurn>("TurnUI");
        Managers.UI.HideUI(actUI.gameObject);
        if (batchUI != null)
        {
            Managers.Prefab.Destroy(batchUI.gameObject);
        }
        if (repairUI != null)
        {
            Managers.InvenUI.isRepair = false;
            Managers.Prefab.Destroy(repairUI.gameObject);
        }
        if (saveUI != null)
        {
            Managers.Prefab.Destroy(saveUI.gameObject);
        }
        Managers.Prefab.Destroy(gameObject);
    }

    public void OnBatchButton(PointerEventData data)
    {
        if(batchUI == null)
        {
            batchUI = Managers.UI.CreatePopupUI<BatchUI>("BatchUI");
            Managers.BattleUI.batchUI = batchUI;
        }
        batchUI.preBattleUI = gameObject;
        Managers.UI.ShowUI(batchUI.gameObject);
        Managers.UI.HideUI(gameObject);
        batchUI.SetMapInfo();
    }

    public void OnRepairButton(PointerEventData data)
    {
        if(repairUI == null)
        {
            repairUI = Managers.UI.CreatePopupUI<UI_Repair>("RepairUI");
            Managers.InvenUI.SetInventory();
        }
        Managers.UI.ShowUI(repairUI.gameObject);
        // repairUI.UpdatePlayerInfo();
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

    public void OnCameraButton(PointerEventData data)
    {
        if (Managers.BattleUI.cameraMode != CameraMode.Move)
        {
            cameraController.ChangeCameraMode(CameraMode.Move, false, true);
            Managers.BattleUI.cameraMode = CameraMode.Move;
        }
        else if(Managers.BattleUI.cameraMode != CameraMode.Follow)
        {
            cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
            Managers.BattleUI.cameraMode = CameraMode.Follow;
        }
        else if (Managers.BattleUI.cameraMode != CameraMode.UI)
        {
            cameraController.ChangeCameraMode(CameraMode.UI, false, true);
            Managers.BattleUI.cameraMode = CameraMode.UI;
        }
    }
}
