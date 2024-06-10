using UnityEngine;
using UnityEngine.EventSystems;

public class BatchUI : UI_Popup
{
    enum batchUI
    {
        PlayerSpawn,
        MonsterSpawn,
        CrabSpawn,
        LizardSpawn,
        CancleButton,
        FinishButton
    }

    public GameObject preBattleUI;

    public GameObject playerSpawn;
    public GameObject monsterSpawn;
    public GameObject cancleBtn;
    public GameObject finishBtn;

    //Test
    public GameObject crabSpawn;
    public GameObject lizardSpawn;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(batchUI));

        playerSpawn = GetObject((int)batchUI.PlayerSpawn);
        monsterSpawn = GetObject((int)batchUI.MonsterSpawn);
        cancleBtn = GetObject((int)batchUI.CancleButton);
        finishBtn = GetObject((int)batchUI.FinishButton);
        crabSpawn = GetObject((int)batchUI.CrabSpawn);
        lizardSpawn = GetObject((int)batchUI.LizardSpawn);

        BindEvent(playerSpawn, PlayerSpawn, Define.UIEvent.Click);
        BindEvent(monsterSpawn, MonsterSpawn, Define.UIEvent.Click);
        BindEvent(cancleBtn, CancleButtonClick, Define.UIEvent.Click);
        BindEvent(finishBtn, FinishButtonClick, Define.UIEvent.Click);

        BindEvent(crabSpawn, CrabSpawn, Define.UIEvent.Click);
        BindEvent(lizardSpawn, LizardSpawn, Define.UIEvent.Click);

        Managers.UI.ShowUI(playerSpawn);
        Managers.UI.ShowUI(monsterSpawn);
        Managers.UI.ShowUI(finishBtn);
        Managers.UI.HideUI(cancleBtn);

        Managers.UI.HideUI(crabSpawn);
        Managers.UI.HideUI(lizardSpawn);
    }

    public void PlayerSpawn(PointerEventData data)
    {
        Debug.Log("Player Spawn");
        Managers.UI.ShowUI(cancleBtn);
        Managers.UI.HideUI(playerSpawn);
        Managers.UI.HideUI(monsterSpawn);
        Managers.UI.HideUI(finishBtn);

        // TODO : 생성하는 Player가 2명 이상일 때 수정 필요
        Managers.BattleUI.player = Managers.Party.playerParty[0];
        if (Managers.BattleUI.player == null)
        {
            Debug.Log("Player Null");
        }
        Managers.UI.uiState = UIState.PlayerSet;
    }

    public void MonsterSpawn(PointerEventData data)
    {
        Debug.Log("Monster Spawn");
        Managers.UI.ShowUI(cancleBtn);
        Managers.UI.HideUI(playerSpawn);
        Managers.UI.HideUI(monsterSpawn);
        Managers.UI.HideUI(finishBtn);

        Managers.UI.ShowUI(crabSpawn);
        Managers.UI.ShowUI(lizardSpawn);
    }

    public void CancleButtonClick(PointerEventData data)
    {
        if(Managers.UI.uiState == UIState.PlayerSet)
        {
            Managers.UI.ShowUI(playerSpawn);
            Managers.UI.ShowUI(monsterSpawn);
            Managers.UI.ShowUI(finishBtn);
            Managers.UI.HideUI(cancleBtn);
            Managers.UI.uiState = UIState.Idle;
        }
    }

    public void FinishButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.HideUI(gameObject);
        Managers.UI.ShowUI(preBattleUI);
    }

    public void CrabSpawn(PointerEventData data)
    {
        Managers.UI.HideUI(crabSpawn);
        Managers.UI.HideUI(lizardSpawn);
        // 임시 몬스터 생성
        Managers.BattleUI.player = Managers.Party.monsterParty[0];
        if (Managers.BattleUI.player == null)
        {
            Debug.Log("Monster Null");
        }
        Managers.UI.uiState = UIState.PlayerSet;
    }

    public void LizardSpawn(PointerEventData data)
    {
        Managers.UI.HideUI(crabSpawn);
        Managers.UI.HideUI(lizardSpawn);
        // 임시 몬스터 생성
        Managers.BattleUI.player = Managers.Party.monsterParty[1];
        if (Managers.BattleUI.player == null)
        {
            Debug.Log("Monster Null");
        }
        Managers.UI.uiState = UIState.PlayerSet;
    }
}
