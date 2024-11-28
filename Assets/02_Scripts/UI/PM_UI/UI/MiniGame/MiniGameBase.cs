using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameBase : UI_Popup
{
    protected int stageNumber = 1;
    public override void Init()
    {
        base.Init();
        // ������ - �̴ϰ��� ������ �� Academy ���� �÷��̾� ���� ���߰� ���� �ٵ� �۵��ȵ�;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(false);
    }

    public int GetStageLevel()
    {
        return stageNumber;
    }
    public abstract void GameEnd();
    public abstract void NextLevel();
}
