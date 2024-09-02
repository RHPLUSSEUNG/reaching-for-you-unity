using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : EnemyAI_Base
{
    protected int turnElasped;

    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Egg";
        spriteController = GetComponent<SpriteController>();
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(1, true));
        isTurnEnd = true;

        actDic = new Dictionary<string, float>();    //행동 확률
        actDic.Add("Worker", 0.5f);
        actDic.Add("Soldier", 0.5f);
    }
    public override void BeforeTrunEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackFail()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackSuccess()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }

    public override void OnMoveEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void OnPathFailed()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTargetFoundFail()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTargetFoundSuccess()
    {
        throw new System.NotImplementedException();
    }

    public override void ProceedTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialCheck()
    {
        if (turnElasped >0) 
        {
            switch (RandChoose(actDic))  // 랜덤 액션
            {
                case "Worker":
                    Managers.Party.InstantiateMonster("Worker");
                    // Managers.Party.AddMonster(); //보호 수준으로 접근 불가
                    break;
                case "Soldier":
                    Managers.Party.InstantiateMonster("Soldier");
                    // Managers.Party.AddMonster(); //보호 수준으로 접근 불가
                    break;

               // 턴 종료 후 객체 파괴 필요
            }
            BeforeTrunEnd();
        }
    }
}
