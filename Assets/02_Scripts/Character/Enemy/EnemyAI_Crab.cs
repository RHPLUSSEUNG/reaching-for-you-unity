using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Crab : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        isHide = false;
        hideLapse = 0;
    }
    public int lastDamaged = 0; //Ȯ�ο����� public
    public bool isHide;
    public int hideLapse;
    public override void SpecialCheck()
    {
        if (isHide) //��ũ�� ����
        {
            if (hideLapse < 2)  // ��ų ������ 2�� �̳��� ����
            {
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);   // ���� ����
                hideLapse++;    // �� ����
                TurnEnd();
            }
            else
            {
                isHide = false; // 2�� ������ �� ����
                hideLapse = 0;
            }
        }
        else if (lastDamaged > 50 && stat.ActPoint >= 70 && stat.Mp >= 60)    // �� �Ͽ� ���� �̻� �ǰ� ��
        {
            // ��ũ���� ��ų ����
            Debug.Log("Skill Used!");
            isAttacked = true;
            stat.ActPoint -= 70;
            stat.Mp -= 60;
            lastDamaged = 0;
            isHide = true;
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
        if (!isMoved)
        {
            PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
        }
        else
        {
            TurnEnd();
        }


    }
    public override void OnPathFailed()
    {
        TurnEnd();
    }
    public override void OnMoveEnd()
    {
        SpecialCheck();
        if (isMoved && isAttacked)
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
