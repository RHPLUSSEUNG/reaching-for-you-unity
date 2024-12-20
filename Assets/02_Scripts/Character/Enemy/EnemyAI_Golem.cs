using UnityEngine;

public class EnemyAI_Golem : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Golem";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(7, true));
        fruitCount = 2;
        spriteController.SetAnimState(AnimState.State2);
    }
    public int fruitCount;
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
                Attack(60);
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

        if (fruitCount ==2 && stat.Hp <= stat.MaxHp / 2)
        {
            skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
            fruitCount--;
            spriteController.SetAnimState(AnimState.State1);
            spriteController.SetAnimState(AnimState.Trigger1);
            BeforeTrunEnd();
        }
        else if (fruitCount ==1 && stat.Hp <= stat.MaxHp / 4)
        {
            skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
            fruitCount--;
            spriteController.SetAnimState(AnimState.State0);
            spriteController.SetAnimState(AnimState.Trigger1);
            BeforeTrunEnd();
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
        
    }
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
