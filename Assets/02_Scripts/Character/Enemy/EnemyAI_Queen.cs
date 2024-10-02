using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyAI_Queen : EnemyAI_Base
{
    protected int eggCount;
    protected int layCount;
    protected int atkLapse;
    protected int defLapse;
    protected int eggLapse;
    // ~lapse 해당 스킬 사용 후 지난 턴

    private void Start()
    {
        eggCount = 0;
        layCount = 1;
        atkLapse = defLapse = eggLapse = 3;

        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Queen";
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(2, true));
        skillList.AddSkill(Managers.Skill.InstantiateSkill(3, true));
        skillList.AddSkill(Managers.Skill.InstantiateSkill(4, true));

        SetTargetTag("Monster");

        actDic = new Dictionary<string, float>();
        actDic.Add("AtkUp", 0.5f);
        actDic.Add("DefUp", 0.5f);
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        if (!isAttacked)
        {
            Search(stat.Sight);
        }
    }
    public override void OnTargetFoundSuccess()
    {
        Move();
    }
    public override void OnTargetFoundFail()
    {
        GetRandomLoc(stat.MovePoint);   //가까운 아군 미발견 시 이동범위 랜덤 이동
    }
    public override void OnPathFailed()
    {
        GetRandomLoc(stat.MovePoint);
    }
    public override void OnMoveEnd()
    {
        SpecialCheck();
        BeforeTrunEnd();
    }
    public override void SpecialCheck()
    {
        if (eggCount < 3 && eggLapse > 2)   // 임시 숫자 이하의 알
        {
            for (int i = 0; i < layCount; i++)   // 산란 횟수만큼
            {
                GetRandomLoc(5, true);

            }
            eggLapse = 0;
        }
        else
        {
            if (atkLapse > 2 && defLapse > 2)        // 두 버프 모두 사용 가능할 시
            {
                switch (RandChoose(actDic))  // 랜덤 액션
                {
                    case "AtkUp":   // 공격력 버프 스킬
                        spriteController.SetAnimState(AnimState.Trigger2);
                        skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetPos);
                        atkLapse = 0;
                        break;
                    case "DefUp":   // 방어력 버프 스킬
                        spriteController.SetAnimState(AnimState.Trigger3);
                        skillList.list[1].GetComponent<MonsterSkill>().SetTarget(targetPos);
                        defLapse = 0;
                        break;
                }
                BeforeTrunEnd();
            }
            else if (atkLapse > 2)  //  공격 버프만
            {
                spriteController.SetAnimState(AnimState.Trigger2);
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetPos);
                atkLapse = 0;
            }
            else if (defLapse > 2)   // 방어 버프만
            {
                spriteController.SetAnimState(AnimState.Trigger3);
                skillList.list[1].GetComponent<MonsterSkill>().SetTarget(targetPos);
                defLapse = 0;
            }
        }

        isAttacked = true;
    }
    public override void OnAttackSuccess()
    {
        throw new System.NotImplementedException();
    }
    public override void OnAttackFail()
    {
        throw new System.NotImplementedException();
    }
    public override void BeforeTrunEnd()
    {
        eggLapse++;
        atkLapse++;
        defLapse++;

        TurnEnd();
    }
    public override void RadomTile()
    {
        spriteController.SetAnimState(AnimState.Trigger1);
        skillList.list[2].GetComponent<MonsterSkill>().SetTarget(targetPos);
        //해당 위치에 알 생성

        // [PM_UI] 생성한 알 TurnOrder 반영
        Managers.BattleUI.turnUI.MakeTurnUI();
    }
    public override void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }
}
