using UnityEngine;

public class BattleUIManager
{
    public ActUI actUI;
    public BatchUI batchUI;
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
        // UI ²ô´Â°Å ÇÊ¿ä
    }

    public void SetPosition(GameObject pos)
    {
        player.transform.position = pos.transform.position + new Vector3(0f,0.8f,0);
        Managers.Battle.ObjectList.Add(player);
        Managers.Battle.playerLive++;
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.ShowUI(batchUI.playerSpawn);
        Managers.UI.ShowUI(batchUI.monsterSpawn);
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
