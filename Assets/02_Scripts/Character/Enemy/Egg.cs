using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : EnemyAI_Base
{

    protected int turnElasped;

    private void Start()
    {
        turnElasped = 0;

        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Egg";
        //spriteController = GetComponent<SpriteController>();

        isTurnEnd = true;

        actDic = new Dictionary<string, float>();    //Çàµ¿ È®·ü
        actDic.Add("Worker", 0.5f);
        actDic.Add("Soldier", 0.5f);
    }
    public override void BeforeTrunEnd()
    {
        turnElasped++;
        TurnEnd();
    }

    public override void OnAttackFail()
    {
        
    }

    public override void OnAttackSuccess()
    {
        
    }

    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }

    public override void OnMoveEnd()
    {
        
    }

    public override void OnPathFailed()
    {
        
    }

    public override void OnTargetFoundFail()
    {
        
    }

    public override void OnTargetFoundSuccess()
    {
        
    }

    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        SpecialCheck();
    }

    public override void SpecialCheck()
    {
        if (turnElasped >1) 
        {
            Destroy(gameObject);
        }
        BeforeTrunEnd();
    }

    public override void RadomTile()
    {
        
    }

    private void OnDestroy()
    {
        GameObject newObj = new GameObject();
        switch (RandChoose(actDic))  // ·£´ý ¾×¼Ç
        {
            case "Worker":
                newObj = Managers.Party.InstantiateMonster("Enemy_Worker");
                newObj.transform.position = this.transform.position;
                break;
            case "Soldier":
                newObj = Managers.Party.InstantiateMonster("Enemy_Soldier");
                newObj.transform.position = this.transform.position;
                break;
        }
        Managers.Battle.CameraAllocate(newObj);
        TurnEnd();
    }
}
