using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Crab : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Crab";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        isHide = false;
        hideLapse = 0;
    }
    public int lastDamaged = 0;
    public bool isHide;
    public int hideLapse;
    public override void ProceedTurn()
    {
        if(!isTurnEnd)
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
        BeforeTrunEnd();    //TODO : 이동 경로 확보 불가 시 행동
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
        else if (lastDamaged >= 50 && stat.ActPoint >= 70 && stat.Mp >= 60)
        {
            Debug.Log("Skill Used!");
            spriteController.SetAnimState(AnimState.Trigger1);
            isAttacked = true;
            stat.ActPoint -= 70;
            stat.Mp -= 60;
            lastDamaged = 0;
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

        lastDamaged = 0;
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
        lastDamaged += damage;
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
