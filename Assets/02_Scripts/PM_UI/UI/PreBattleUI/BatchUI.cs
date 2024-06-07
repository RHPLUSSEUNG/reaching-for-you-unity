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

        Managers.UI.ShowUI(playerSpawn);
        Managers.UI.ShowUI(monsterSpawn);
        Managers.UI.ShowUI(finishBtn);
        Managers.UI.HideUI(cancleBtn);
    }

    public void PlayerSpawn(PointerEventData data)
    {
        Debug.Log("Player Spawn");
        Managers.UI.ShowUI(cancleBtn);
        Managers.UI.HideUI(playerSpawn);
        Managers.UI.HideUI(monsterSpawn);
        Managers.UI.HideUI(finishBtn);
        Managers.UI.uiState = UIManager.UIState.PlayerSet;
    }

    public void MonsterSpawn(PointerEventData data)
    {
        Debug.Log("Monster Spawn");
        Managers.UI.ShowUI(cancleBtn);
        Managers.UI.HideUI(playerSpawn);
        Managers.UI.HideUI(monsterSpawn);
        Managers.UI.HideUI(finishBtn);
        Managers.UI.uiState = UIManager.UIState.PlayerSet;
    }

    public void CancleButtonClick(PointerEventData data)
    {
        if(Managers.UI.uiState == UIManager.UIState.PlayerSet)
        {
            Managers.UI.ShowUI(playerSpawn);
            Managers.UI.ShowUI(monsterSpawn);
            Managers.UI.ShowUI(finishBtn);
            Managers.UI.HideUI(cancleBtn);
            Managers.UI.uiState = UIManager.UIState.Idle;
        }
    }

    public void FinishButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIManager.UIState.Idle;
        Managers.UI.HideUI(gameObject);
        Managers.UI.ShowUI(preBattleUI);
    }
}
