using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Soldier : EnemyAI_Base
{
    Dictionary<string, float> atkDebuffDic;
    Dictionary<string, float> skillDebuffDic;
    private void Start()
    {
        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Soldier";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(5, true));

        actDic = new Dictionary<string, float>();    //행동 확률
        actDic.Add("Attack", 0.5f);
        actDic.Add("Skill", 0.5f);

        atkDebuffDic = new Dictionary<string, float>();    //기본 공격 디버프 확률
        atkDebuffDic.Add("Poison", 0.2f);
        atkDebuffDic.Add("None", 0.8f);

        skillDebuffDic = new Dictionary<string, float>();    //기본 공격 디버프 확률
        skillDebuffDic.Add("Poison", 0.15f);
        skillDebuffDic.Add("None", 0.85f);
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
            Search(stat.Sight, RangeType.Normal);
    }
    public override void SpecialCheck()
    {
        if (CanAttack(stat.AttackRange))    //사거리 체크
        {
            switch (RandChoose(actDic))  // 랜덤 액션
            {
                case "Attack":  // 기본 공격
                    Attack(50);
                    switch (RandChoose(atkDebuffDic))  // 기본 공격 중독 확률
                    {
                        case "Poison":  // 중독
                           Poision debuff = new Poision();
                            debuff.SetDebuff(999,targetObj);
                            break;
                        case "None":   // 효과 없음
                            break;
                    }

                    break;
                case "Skill":   // 스킬 사용
                    spriteController.SetAnimState(AnimState.Trigger1);
                    skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
                    switch (RandChoose(skillDebuffDic))  // 스킬 공격 중독 확률
                    {
                        case "Poison":  // 중독
                            Poision debuff = new Poision();
                            debuff.SetDebuff(999, targetObj);
                            break;
                        case "None":   // 효과 없음
                            break;
                    }
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
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
