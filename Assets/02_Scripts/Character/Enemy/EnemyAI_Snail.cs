using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Snail : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Snail";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        isHide = false;
        hideLapse = 0;
    }
    public bool isHide;
    public int hideLapse;
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        SpecialCheck();
        if (!isTurnEnd)
        {
            Search(stat.Sight);
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
                Attack(80);
                Cold debuff = new Cold(); //추위 스탯 부여
                debuff.SetDebuff(2, targetObj); //2스택 부여로 변경 필요
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
        Search(stat.Sight);
    }
    public override void SpecialCheck()
    {
        if (isTurnEnd)
            return;

        if (isHide)
        {
            if (hideLapse < 2)
            {
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
                hideLapse++;
                BeforeTrunEnd();
            }
            else
            {
                spriteController.SetAnimState(AnimState.Trigger2);
                isHide = false;
                hideLapse = 0;
            }
        }
        else if (stat.Hp <= stat.MaxHp*0.4 && stat.ActPoint >= 60 && stat.Mp >= 50)
        {
            Debug.Log("Skill Used!");
            spriteController.SetAnimState(AnimState.Trigger1);
            isAttacked = true;
            stat.ActPoint -= 60;
            stat.Mp -= 50;
            isHide = true;
            skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
            TurnEnd();
        }
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
