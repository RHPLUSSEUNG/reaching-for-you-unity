using UnityEngine;

public class EnemyAI_Crab : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.Instantiate(0, true));
    }
    public int lastDamaged = 0;
    public override void SpecialCheck()
    {
        if (lastDamaged > 50 && stat.ActPoint >= 70 && stat.Mp >= 60)    //한 턴에 일정 이상 피격 시
        {
            // 웅크리기 스킬 사용
            Debug.Log("Skill Used!");
            isAttacked = true;
            stat.ActPoint -= 70;
            stat.Mp -= 60;
            lastDamaged = 0;
            skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
            TurnEnd();
        }
    }
    public override void ProceedTurn()
    {
        if(!isTurnEnd)
        {
            return;
        }
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
                Attack(30);
            }
            else
            {
                TurnEnd();
            }
        }
    }
    public override void OnTargetFoundFail()
    {
        if (isTurnEnd)
            return;
        PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
    }
    public override void OnPathFailed()
    {
        TurnEnd();
    }
    public override void OnMoveEnd()
    {
        SpecialCheck();
        if (isMoved)
            TurnEnd();
        Search(stat.Sight);
    }
    public override void OnAttackSuccess()
    {
        TurnEnd();
    }
    public override void OnAttackFail()
    {
        TurnEnd();
    }
    public override void OnHit(int damage)
    {
        stat.Hp -= damage;
        lastDamaged += damage;
    }
}
