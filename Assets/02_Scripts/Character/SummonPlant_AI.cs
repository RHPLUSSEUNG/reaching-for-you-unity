using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPlant_AI : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Plant";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;

        SetTargetTag("Enemy");
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        if (!isTurnEnd)
        {
            Search(stat.Sight, RangeType.Normal);
        }
    }
    public override void OnTargetFoundSuccess()
    {
        if (isTurnEnd)
            return;

        else
        {
            if (CanAttack(stat.AttackRange))
            {
                PathFinder.RequestSkillRange(transform.position, stat.AttackRange, RangeType.Normal, OnSkillRangeFound);
                Attack(30);
                Poision debuff = new Poision();
                debuff.SetDebuff(999, targetObj);
                //독 적용 추가
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

        BeforeTrunEnd();
    }
    public override void OnPathFailed()
    {
        BeforeTrunEnd();
    }
    public override void OnMoveEnd()
    {
        BeforeTrunEnd();
    }
    public override void SpecialCheck()
    {
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
        BeforeTrunEnd();
    }
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
