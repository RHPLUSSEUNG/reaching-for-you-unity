using UnityEngine;

public class EnemyAI_Lizard : EnemyAI_Base
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    protected bool isSIzeMode = false;

    Transform tailPos;
    LayerMask coverLayer;

    private void Start()
    {
        coverLayer = 1 << LayerMask.NameToLayer("EnvironmentObject");

        stat = GetComponent<EnemyStat>();
        stat.enemyName = "Lizard";
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
                foreach(var hit in Physics.RaycastAll(transform.position, dir, targetDistance, coverLayer))
                {
                    if (hit.collider.gameObject.CompareTag("Cover"))
                    {
                        cover = hit.collider.gameObject;
                    }
                }
                    Attack(50);

            }
            else if (targetDistance <= 2) // 장전상태가 아니고, 적이 2칸 이내에 있을 경우
            {
                PathFinder.RequestSkillRange(transform.position, 2, RangeType.Cross, OnSkillRangeFound);
                Debug.Log("Skill Used!");
                stat.ActPoint -= 60;
                stat.Mp -= 60;
                spriteController.SetAnimState(AnimState.Trigger2);
                skillList.list[0].GetComponent<MonsterSkill>().SetTarget(targetObj.transform.parent.gameObject);
                BeforeTrunEnd();
            }
            else  // 장전상태가 아니고, 적이 2칸 밖에 있을 경우
            {
                spriteController.SetAnimState(AnimState.Move);  // Idle 상태 복귀 방지를 위한 변경
                spriteController.SetAnimState(AnimState.Trigger1);
                isAttacked = true;
                stat.ActPoint -= 50;
                isSIzeMode = true;
                Debug.Log("turnend");
                TurnEnd();
            }
        }
        else // 사거리 안에 적이 없을 경우
        {
            isAttacked = true;
            isMoved = true;
            spriteController.SetAnimState(AnimState.Move);  // Idle 상태 복귀 방지를 위한 변경
            spriteController.SetAnimState(AnimState.Trigger1);
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
            spriteController.SetAnimState(AnimState.Idle);
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
            // 돌 없는 애니메이션 재생
            spriteController.SetAnimState(AnimState.Idle);
            GetRandomLoc(stat.MovePoint);
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
