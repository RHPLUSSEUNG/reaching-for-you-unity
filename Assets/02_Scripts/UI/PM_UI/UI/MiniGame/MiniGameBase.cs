using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameBase : UI_Popup
{
    protected int stageNumber = 1;
    public override void Init()
    {
        base.Init();
        // 권희준 - 미니게임 시작할 때 Academy 씬의 플레이어 조작 멈추고 싶음 근데 작동안됨;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(false);
    }

    public int GetStageLevel()
    {
        return stageNumber;
    }
    public abstract void GameEnd();
    public abstract void NextLevel();
}
