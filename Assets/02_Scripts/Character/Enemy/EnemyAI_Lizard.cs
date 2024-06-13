using UnityEngine;

public class EnemyAI_Lizard : EnemyAI_Base
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    protected bool isSIzeMode = false;

    Transform tailPos;

    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(1, true));
        isTurnEnd = true;
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
        {
            return;
        }
        OnTurnStart();
        if (!isTurnEnd)
        {
            Search(stat.Sight);
        }

    }
    public override void SpecialCheck()
    {
        if (CanAttack(stat.AttackRange))
        {
            if (isSIzeMode) //�� ���� ��
            {
                isSIzeMode = false;
                tailPos = this.transform.GetChild(0).transform.GetChild(2);
                projectile.GetComponent<ArcProjectile>().Shoot(tailPos, targetObj.transform);
                Attack(50);
            }
            else if (targetDistance <= 2) // �� ������ X, ������ 2ĭ ��
            {
                Debug.Log("Skill Used!");
                stat.ActPoint -= 60;
                stat.Mp -= 60;
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
                TurnEnd();
            }
            else  // �� ������ X, ������ 2ĭ ��
            {
                isAttacked = true;
                stat.ActPoint -= 50;
                isSIzeMode = true;
                Debug.Log("turnend");
                TurnEnd();
            }
        }
        else // ������ ���� ���� ��
        {
            isAttacked = true;
            stat.ActPoint -= 50;
            isSIzeMode = true;
            TurnEnd();
        }
    }
    public override void OnMoveEnd()
    {
        if (isMoved && isAttacked)
            TurnEnd();
        if (!isAttacked)
        {
            SpecialCheck();
        }
        else
            TurnEnd();
    }
    public override void OnHit(int damage)
    {
        stat.Hp -= damage;
        if (isSIzeMode)
        {
            isSIzeMode = false;
        }
    }
    public override void OnTargetFoundSuccess()
    {
        if (!isAttacked)
            SpecialCheck();
        else
            TurnEnd();
    }
    public override void OnTargetFoundFail()
    {
        if (!isMoved)
        {
            isSIzeMode = false;
            PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
        }
        else
            TurnEnd();
    }
    public override void OnPathFailed()
    {
        TurnEnd();
    }
    public override void OnAttackSuccess()
    {
        TurnEnd();
    }
    public override void OnAttackFail()
    {
        TurnEnd();
    }
}
