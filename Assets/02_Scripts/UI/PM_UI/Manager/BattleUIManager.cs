using UnityEngine;

public class BattleUIManager
{
    public ActUI actUI;
    public BatchUI batchUI;
    public BattleUI battleUI;
    public UI_ActTurn turnUI;
    public WarningUI warningUI;
    public BattleInfoUI battleInfoUI;

    public GameObject actPanel;
    public GameObject magicPanel;
    public GameObject itemPanel;
    public GameObject descriptPanel;
    public GameObject moveBtn;
    public GameObject cancleBtn;

    public GameObject player;
    public GameObject skill;
    public int skill_ID;
    public GameObject item;
    public int item_ID;
    public CameraMode cameraMode;

    public Active GetSkill()
    {
        if (skill == null || skill.GetComponent<Active>() == null)
        {
            return null;
        }
        return skill.GetComponent<Active>();
    }

    public Consume GetItem()
    {
        if (item == null || item.GetComponent<Consume>() == null)
        {
            return null;
        }
        return item.GetComponent<Consume>();
    }

    public void ShowCharacterInfo(GameObject hit)
    {
        if(battleInfoUI == null)
        {
            battleInfoUI = Managers.UI.CreatePopupUI<BattleInfoUI>("CharacterInfoUI");
        }
        battleInfoUI.SetInfo(hit);
        Managers.UI.ShowUI(battleInfoUI.gameObject);
        Managers.BattleUI.battleInfoUI.SetPosition();
    }

    public void SetPosition(GameObject pos)
    {
        GameObject mapSpawner = GameObject.Find("MapSpawner");      // 임시, 변경 필요 : 위치 이동 or 색 비교
        CreateObject mapInfo = mapSpawner.GetComponent<CreateObject>();
        int x = (int)pos.transform.position.x;
        int z = (int)pos.transform.position.z;
        bool spawnCheck = mapInfo.IsEnemySpawnPosition(x,z);
        if (spawnCheck)
        {
            Managers.BattleUI.warningUI.SetText("생성할 수 없는 구역입니다.");
            Managers.BattleUI.warningUI.ShowWarningUI();
            return;
        }
        player.transform.position = pos.transform.position + new Vector3(0f,0.8f,0);
        if (!Managers.Battle.ObjectList.Contains(player))
        {
            Managers.Battle.ObjectList.Add(player);
            Managers.Battle.playerLive++;
        }
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.ShowUI(batchUI.playerSpawn);
        Managers.UI.ShowUI(batchUI.finishBtn);
        Managers.UI.HideUI(batchUI.cancleBtn);
    }

    public void PlayerMovePhaseUI()
    {
        Managers.UI.ShowUI(moveBtn);
        Managers.UI.ShowUI(Managers.BattleUI.turnUI.turnUIBtn.gameObject);
        Managers.UI.HideUI(actPanel);
        Managers.UI.HideUI(descriptPanel);
        Managers.UI.HideUI(cancleBtn);
    }

    public void PlayerBattlePhaseUI()
    {
        Managers.UI.HideUI(moveBtn);
        Managers.UI.HideUI(cancleBtn);
        Managers.UI.ShowUI(actPanel);
        Managers.UI.ShowUI(magicPanel);
    }

    public void PlayerActPhaseUI()
    {
        Managers.UI.HideUI(Managers.BattleUI.descriptPanel);
        Managers.UI.HideUI(Managers.BattleUI.actPanel);
        Managers.UI.ShowUI(Managers.BattleUI.cancleBtn);
    }

    public void PlayerPhaseEndUI()
    {
        Managers.UI.HideUI(Managers.BattleUI.cancleBtn);
        Managers.UI.HideUI(Managers.BattleUI.turnUI.turnUIBtn.gameObject);
    }
}
