using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Worker : EnemyAI_Base
{
    private int flyLapse;
    private void Start()
    {
        flyLapse = 3;

        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Worker";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(6, true));

        actDic = new Dictionary<string, float>();   //�ൿ Ȯ��
        actDic.Add("Attack", 0.5f);
        actDic.Add("Skill", 0.5f);
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        Search(stat.Sight, RangeType.Normal);
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
            BeforeTrunEnd();
    }
    public override void OnPathFailed()
    {
        BeforeTrunEnd();    //TODO : �̵� ��� Ȯ�� �Ұ� �� �ൿ
    }
    public override void OnMoveEnd()
    {
        if (isTurnEnd)
            return;

        if (isMoved && isAttacked)
            BeforeTrunEnd();
        else
            Search(stat.Sight, RangeType.Normal);
    }
    public override void SpecialCheck()
    {
        // Ȯ���� ���� or ȸ�� ��ų ��� ����
        if (CanAttack(stat.AttackRange))        // ��Ÿ� üũ
        {
            if (flyLapse > 2)
            {
                switch (RandChoose(actDic))  // ���� �׼�
                {
                    case "Attack":  // �⺻ ����
                        Attack(30);
                        break;
                    case "Skill":   // ��ų ���
                        spriteController.SetAnimState(AnimState.Trigger1);
                        skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
                        flyLapse = 0;
                        break;
                }
            }
            else
                Attack(30);
        }
        else if (isTargetFound)
        {
            if (flyLapse > 2)   // ��ų ���
            {
                spriteController.SetAnimState(AnimState.Trigger1);
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(gameObject);
                flyLapse = 0;
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
        flyLapse++;
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
