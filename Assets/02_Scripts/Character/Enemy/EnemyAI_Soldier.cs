using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Soldier : EnemyAI_Base
{
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Soldier";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(5, true));

        actDic = new Dictionary<string, float>();    //�ൿ Ȯ��
        actDic.Add("Attack", 0.5f);
        actDic.Add("Skill", 0.5f);
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        Search(stat.Sight);
    }
    public override void OnTargetFoundSuccess()
    {
        if (isTurnEnd)
            return;

        if (!isMoved)
            Move();
        else
            SpecialCheck();
    }
    public override void OnTargetFoundFail()
    {
        if (isTurnEnd)
            return;

        if (!isMoved)
            GetRandomLoc(stat.MovePoint);
        else
            SpecialCheck();
    }
    public override void OnPathFailed()
    {
        GetRandomLoc(stat.MovePoint);
    }
    public override void OnMoveEnd()
    {
        if (isTurnEnd)
            return;

        if (isMoved && isAttacked)
            BeforeTrunEnd();
        else
            Search(stat.Sight);
    }
    public override void SpecialCheck()
    {
        if (CanAttack(stat.AttackRange))    //��Ÿ� üũ
        {
            switch (RandChoose(actDic))  // ���� �׼�
            {
                case "Attack":  // �⺻ ����
                    Attack(50);
                    break;
                case "Skill":   // ��ų ���
                    spriteController.SetAnimState(AnimState.Trigger1);
                    skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
                    break;
            }
        }
        BeforeTrunEnd();
    }
    public override void OnAttackSuccess()
    {
    }
    public override void OnAttackFail()
    {
    }
    public override void BeforeTrunEnd()
    {
        TurnEnd();
    }
    public override void RadomTile()
    {
    }
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }
}
