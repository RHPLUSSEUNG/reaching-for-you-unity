using UnityEngine;
using UnityEngine.EventSystems;

public class BatchUI : UI_Popup
{
    enum batchUI
    {
        PlayerSpawn,
        MonsterSpawn,
        CancleButton,
        FinishButton
    }

    public GameObject preBattleUI;

    GameObject playerSpawn;
    GameObject monsterSpawn;
    GameObject cancleBtn;
    GameObject finishBtn;
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(batchUI));

        playerSpawn = GetObject((int)batchUI.PlayerSpawn);
        monsterSpawn = GetObject((int)batchUI.MonsterSpawn);
        cancleBtn = GetObject((int)batchUI.CancleButton);
        finishBtn = GetObject((int)batchUI.FinishButton);
        BindEvent(playerSpawn, PlayerSpawn, Define.UIEvent.Click);
        BindEvent(monsterSpawn, MonsterSpawn, Define.UIEvent.Click);
        BindEvent(cancleBtn, CancleButtonClick, Define.UIEvent.Click);
        BindEvent(finishBtn, FinishButtonClick, Define.UIEvent.Click);

        PM_UI_Manager.UI.ShowUI(playerSpawn);
        PM_UI_Manager.UI.ShowUI(monsterSpawn);
        PM_UI_Manager.UI.ShowUI(finishBtn);
        PM_UI_Manager.UI.HideUI(cancleBtn);
    }

    public void PlayerSpawn(PointerEventData data)
    {
        Debug.Log("Player Spawn");
        PM_UI_Manager.UI.ShowUI(cancleBtn);
        PM_UI_Manager.UI.HideUI(playerSpawn);
        PM_UI_Manager.UI.HideUI(monsterSpawn);
        PM_UI_Manager.UI.HideUI(finishBtn);
        PM_UI_Manager.UI.uiState = UIManager.UIState.PlayerSet;
    }

    public void MonsterSpawn(PointerEventData data)
    {
        Debug.Log("Monster Spawn");
        PM_UI_Manager.UI.ShowUI(cancleBtn);
        PM_UI_Manager.UI.HideUI(playerSpawn);
        PM_UI_Manager.UI.HideUI(monsterSpawn);
        PM_UI_Manager.UI.HideUI(finishBtn);
        PM_UI_Manager.UI.uiState = UIManager.UIState.PlayerSet;
    }

    public void CancleButtonClick(PointerEventData data)
    {
        if(PM_UI_Manager.UI.uiState == UIManager.UIState.PlayerSet)
        {
            PM_UI_Manager.UI.ShowUI(playerSpawn);
            PM_UI_Manager.UI.ShowUI(monsterSpawn);
            PM_UI_Manager.UI.ShowUI(finishBtn);
            PM_UI_Manager.UI.HideUI(cancleBtn);
            PM_UI_Manager.UI.uiState = UIManager.UIState.Idle;
        }
    }

    public void FinishButtonClick(PointerEventData data)
    {
        PM_UI_Manager.UI.uiState = UIManager.UIState.Idle;
        PM_UI_Manager.UI.HideUI(gameObject);
        PM_UI_Manager.UI.ShowUI(preBattleUI);
    }
}
