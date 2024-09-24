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
        //skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        fruitCount = 2;
    }
    public int fruitCount;
    public override void SpecialCheck()
    {
        if (isTurnEnd)
            return;

        if (stat.Hp <= stat.MaxMp/2) 
        {
            if (fruitCount > 0) 
            {
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
                fruitCount--;
                BeforeTrunEnd();
            }
        }
    }
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
        if (isTurnEnd)
            return;

        BeforeTrunEnd();
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
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }
    public override void BeforeTrunEnd()
    {
        if (isTurnEnd)
            return;

        TurnEnd();
    }

    public override void RadomTile()
    {
        throw new System.NotImplementedException();
    }
}
