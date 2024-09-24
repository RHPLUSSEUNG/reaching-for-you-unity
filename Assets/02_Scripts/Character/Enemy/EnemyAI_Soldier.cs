using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Soldier : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Soldier";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        //skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));

        actDic = new Dictionary<string, float>();    //행동 확률
        actDic.Add("Attack", 0.5f);
        actDic.Add("Skill", 0.5f);
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
        switch (RandChoose(actDic))  // 랜덤 액션
        {
            case "Attack":
                // 기본 공격
                Attack(50);
                break;
            case "Skill":
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
                // 스킬 사용
                break;
        }
        BeforeTrunEnd();
    }

    public override void RadomTile()
    {
        throw new System.NotImplementedException();
    }
}
