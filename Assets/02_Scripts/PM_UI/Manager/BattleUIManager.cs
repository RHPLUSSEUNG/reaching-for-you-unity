using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager
{
    public ActUI actUI;
    public BatchUI batchUI;
    public GameObject magicPanel;
    public GameObject itemPanel;
    public GameObject descriptPanel;
    public GameObject cancleBtn;

    public GameObject player;
    public GameObject skill;
    public Item item;

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

    public void SetPosition(GameObject pos)
    {
        player.transform.position = pos.transform.position + Vector3.up;
        Managers.UI.uiState = UIState.Idle;

        // TODO : 배치 UI 재생성
        Managers.UI.ShowUI(batchUI.playerSpawn);
        Managers.UI.ShowUI(batchUI.monsterSpawn);
        Managers.UI.ShowUI(batchUI.finishBtn);
        Managers.UI.HideUI(batchUI.cancleBtn);
    }

}
