using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Queen : EnemyAI_Base
{
    protected int egg_Count;

    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        //skillList.AddSkill(Managers.Skill.InstantiateSkill(0, true));
        SetTargetTag("Monster");
    }
    public override void BeforeTrunEnd()
    {
        TurnEnd();
    }

    public override void OnAttackFail()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackSuccess()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }

    public override void OnMoveEnd()
    {
        // ���ݷ� or ���� ���� ��ų ���
        BeforeTrunEnd();
    }

    public override void OnPathFailed()
    {
        OnMoveEnd();
    }

    public override void OnTargetFoundFail()
    {
        GetRandomLoc(stat.MovePoint);   //����� �Ʊ� �̹߰� �� �̵����� ���� �̵�
    }

    public override void OnTargetFoundSuccess()
    {
        Move();
    }

    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        SpecialCheck();
        if (!isAttacked)
        {
            Search(stat.Sight);
        }
    }

    public override void SpecialCheck()
    {
        if(egg_Count < 3)   // �ӽ� ����
        {
            GetRandomLoc(5);
            //�ش� ��ġ�� �� ����
            isAttacked = true;
        }
    }
}
