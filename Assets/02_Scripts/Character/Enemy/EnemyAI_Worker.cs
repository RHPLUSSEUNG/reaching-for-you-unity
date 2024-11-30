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

        actDic = new Dictionary<string, float>();   //행동 확률
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
        BeforeTrunEnd();    //TODO : 이동 경로 확보 불가 시 행동
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
        // 확률로 공격 or 회피 스킬 사용 결정
        if (CanAttack(stat.AttackRange))        // 사거리 체크
        {
            if (flyLapse > 2)
            {
                switch (RandChoose(actDic))  // 랜덤 액션
                {
                    case "Attack":  // 기본 공격
                        Attack(30);
                        break;
                    case "Skill":   // 스킬 사용
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
            if (flyLapse > 2)   // 스킬 사용
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
