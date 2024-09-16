using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Queen : EnemyAI_Base
{
    protected int eggCount;
    protected int layCount;

    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Queen";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        //skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        SetTargetTag("Monster");
    }
    public override void BeforeTrunEnd()
    {
        TurnEnd();
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
        // 공격력 or 방어력 증가 스킬 사용
        BeforeTrunEnd();
    }

    public override void OnPathFailed()
    {
        OnMoveEnd();
    }

    public override void OnTargetFoundFail()
    {
        GetRandomLoc(stat.MovePoint);   //가까운 아군 미발견 시 이동범위 랜덤 이동
    }

    public override void OnTargetFoundSuccess()
    {
        Move();
    }

    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        SpecialCheck();
        if (!isAttacked)
        {
            Search(stat.Sight);
        }
    }

    public override void SpecialCheck()
    {
        if(eggCount < 3)   // 임시 숫자
        {
            for (int i=0;i<layCount; i++)   // 산란 횟수만큼
            {
                GetRandomLoc(5);
                //skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetPos);
                //해당 위치에 알 생성
            }
            isAttacked = true;
        }
    }
}
