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
            return;

        OnTurnStart();
        Search(stat.Sight);
    }
    public override void SpecialCheck()
    {
        if (isTurnEnd)
            return;

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
                BeforeTrunEnd();
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
            isMoved = true;
            stat.ActPoint -= 50;
            isSIzeMode = true;
            BeforeTrunEnd();
        }
    }
    public override void OnMoveEnd()
    {
        if (isTurnEnd)
            return;

        if (isAttacked)
            BeforeTrunEnd();
        else
        {
            SpecialCheck();
        }
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
        if (isTurnEnd)
            return;

        if (!isAttacked)
            SpecialCheck();
        else
            BeforeTrunEnd();
    }
    public override void OnTargetFoundFail()
    {
        if (isTurnEnd)
            return;

        if (!isMoved)
        {
            isSIzeMode = false;
            PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
        }
        else
            BeforeTrunEnd();
    }
    public override void OnPathFailed()
    {
        if (isTurnEnd)
            return;

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
        TurnEnd();
    }
}
