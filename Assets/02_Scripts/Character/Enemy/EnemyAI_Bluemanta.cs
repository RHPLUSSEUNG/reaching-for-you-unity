using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Bluemanta : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Bluemanta";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        SpecialCheck();
        if (!isTurnEnd)
        {
            Search(stat.Sight, RangeType.Normal);
        }
    }
    public override void OnTargetFoundSuccess()
    {
        if (isTurnEnd)
            return;

        if (!isMoved)
            Move();
        else
        {
            if (CanAttack(stat.AttackRange))
            {
                PathFinder.RequestSkillRange(transform.position, stat.AttackRange, RangeType.Normal, OnSkillRangeFound);
                Attack(30);
            }
            else
            {
                BeforeTrunEnd();
            }
        }
    }
    public override void OnTargetFoundFail()
    {
        if (isTurnEnd)
            return;

        if (!isMoved)
        {
            GetRandomLoc(stat.MovePoint);
        }
        else
        {
            BeforeTrunEnd();
        }
    }
    public override void OnPathFailed()
    {
        GetRandomLoc(stat.MovePoint);
    }
    public override void OnMoveEnd()
    {
        if (isTurnEnd)
            return;

        SpecialCheck();
        if (isMoved && isAttacked)
            BeforeTrunEnd();
        Search(stat.Sight, RangeType.Normal);
    }
    public override void SpecialCheck() //TODO : 스킬 사용 위치 결정 로직 필요
    {
        if (isTurnEnd)
            return;

        PathFinder.RequestSkillRange(transform.position, 3, RangeType.Normal, OnSkillRangeFound);
        stat.ActPoint -= 60;
        stat.Mp -= 60;
        spriteController.SetAnimState(AnimState.Trigger1);
        skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
        BeforeTrunEnd();
    }
    public override void OnAttackSuccess()
    {
        if (isTurnEnd)
            return;

        BeforeTrunEnd();
    }
    public override void OnAttackFail()
    {
        if (isTurnEnd)
            return;

        BeforeTrunEnd();
    }
    public override void BeforeTrunEnd()
    {
        if (isTurnEnd)
            return;

        TurnEnd();
    }
    public override void RadomTile()
    {
        return;
    }
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
