using UnityEngine;

public class EnemyAI_Lizard : EnemyAI_Base
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    protected bool isSIzeMode = false;
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
            if (isSIzeMode) //돌 장전 중
            {
                isSIzeMode = false;
                Attack(50);
            }
            else if (targetDistance <= 2) // 돌 장전중 X, 대상이 2칸 내
            {
                Debug.Log("Skill Used!");
                Attack(60);
                stat.Mp -= 60;
            }
            else  // 돌 장전중 X, 대상이 2칸 밖
            {
                isAttacked = true;
                stat.ActPoint -= 50;
                isSIzeMode = true;
                TurnEnd();
            }
        }
        else // 대상이 공격 범위 밖
        {
            isAttacked = true;
            stat.ActPoint -= 50;
            isSIzeMode = true;
            TurnEnd();
        }
    }
    public override void OnMoveEnd()
    {
        if (isTurnEnd)
            return;
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
        projectile.GetComponent<ArcProjectile>().Shoot(transform, targetObj.transform);
    }
    public override void OnAttackFail()
    {
        TurnEnd();
    }
}
