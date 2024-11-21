using UnityEngine;

public class EnemyAI_Lizard : EnemyAI_Base
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    protected bool isSIzeMode = false;

    Transform tailPos;
    LayerMask coverLayer;

    bool skillchecked;
    private void Start()
    {
        coverLayer = 1 << LayerMask.NameToLayer("EnvironmentObject");

        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Lizard";
        spriteController = GetComponent<SpriteController>();
        skillList = GetComponent<SkillList>();
        skillList.AddSkill(Managers.Skill.InstantiateSkill(1, true));
        isTurnEnd = true;
        skillchecked = false;
    }
    public override void ProceedTurn()
    {
        if (!isTurnEnd)
            return;

        OnTurnStart();
        Search(2, RangeType.Cross);    //스킬 범위 먼저 체크
    }
    public override void OnTargetFoundSuccess()
    {
        if (isTurnEnd)
            return;
        if (!skillchecked)   // 스킬 사용
        {
            PathFinder.RequestSkillRange(transform.position, 2, RangeType.Cross, OnSkillRangeFound);
            Debug.Log("Skill Used!");
            stat.ActPoint -= 60;
            stat.Mp -= 60;
            spriteController.SetAnimState(AnimState.Trigger2);
            skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
            BeforeTrunEnd();
        }
        if (!isAttacked)
            SpecialCheck();
        else
            BeforeTrunEnd();
    }
    public override void OnTargetFoundFail()
    {
        if (isTurnEnd)
            return;
        if (!skillchecked)
        {
            skillchecked = true;
            Search(stat.Sight, RangeType.Normal);
        }

        if (!isMoved)
        {
            isSIzeMode = false;
            // 돌 없는 애니메이션 재생
            spriteController.SetAnimState(AnimState.Idle);
            GetRandomLoc(stat.MovePoint);
        }
        else
            BeforeTrunEnd();
    }
    public override void OnPathFailed()
    {
        GetRandomLoc(stat.MovePoint);
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
    public override void SpecialCheck()
    {
        if (isTurnEnd)
            return;

        if (isTargetFound)  //타겟 발견 시
        {
            if (isSIzeMode) //사거리 안에 적이 있고, 장전상태일 경우
            {
                PathFinder.RequestSkillRange(transform.position, stat.AttackRange, RangeType.Normal, OnSkillRangeFound);
                isSIzeMode = false;
                tailPos = this.transform.GetChild(0).transform.GetChild(2);
                projectile.SetActive(true);
                projectile.GetComponent<ArcProjectile>().Shoot(tailPos, targetObj.transform);

                //raycast 엄폐물 체크
                Vector3 dir = targetObj.transform.position - transform.position;

                cover = null;
                foreach (var hit in Physics.RaycastAll(transform.position, dir, targetDistance, coverLayer))
                {
                    if (hit.collider.gameObject.CompareTag("Cover"))
                    {
                        cover = hit.collider.gameObject;
                        Debug.Log("cover detected");
                    }
                }
                Attack(50);
                spriteController.SetAnimState(AnimState.Idle);
            }
            else  // 장전상태가 아니고, 적이 2칸 밖에 있을 경우
            {
                spriteController.SetAnimState(AnimState.Move);  // Idle 상태 복귀 방지를 위한 변경
                spriteController.SetAnimState(AnimState.Trigger1);
                isAttacked = true;
                stat.ActPoint -= 50;
                isSIzeMode = true;
                BeforeTrunEnd();
            }
        }
        else
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
        skillchecked = false;
        TurnEnd();
    }
    public override void RadomTile()
    {
        BeforeTrunEnd();
    }
    public override void OnHit(int damage)
    {
        stat.Hp -= damage;
        spriteController.SetAnimState(AnimState.Hit);

        if (isSIzeMode)
        {
            isSIzeMode = false;
            spriteController.SetAnimState(AnimState.Idle);
        }
        Managers.BattleUI.ShowDamageUI(damage, transform);
    }
}
