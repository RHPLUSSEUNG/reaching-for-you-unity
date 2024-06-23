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
    public Item item;
    public CameraMode cameraMode;

    public Active GetSkill()
    {
        if (skill == null || skill.GetComponent<Active>() == null)
        {
            return null;
        }
        return skill.GetComponent<Active>();
    }

    public Item GetItem()
    {
        if (item == null)
        {
            return null;
        }
        return item;
    }

    public void ShowCharacterInfo(GameObject hit)
    {
        if(battleInfoUI == null)
        {
            battleInfoUI = Managers.UI.CreatePopupUI<BattleInfoUI>("BattleInfoUI");
        }
        GameObject character = hit.transform.parent.gameObject;
        battleInfoUI.SetInfo(character);
        Managers.UI.ShowUI(battleInfoUI.gameObject);
        // UI 끄는거 필요
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
        Managers.Battle.ObjectList.Add(player);
        Managers.Battle.playerLive++;
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.ShowUI(batchUI.playerSpawn);
        Managers.UI.ShowUI(batchUI.finishBtn);
        Managers.UI.HideUI(batchUI.cancleBtn);
    }

    public void PlayerMovePhaseUI()
    {
        Managers.UI.ShowUI(moveBtn);
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
}
