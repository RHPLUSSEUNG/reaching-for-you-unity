using System.Collections;
using UnityEngine;

public abstract class EnemyAI_Test : MonoBehaviour
{

    [SerializeField]
    float speed = 10;
    [SerializeField]
    string targetTag = "Player";

    protected EnemyStat stat;
    GameObject targetObj;
    SpriteController spriteController;

    Vector3[] path;
    Vector3 targetPos;
    protected int targetIndex;
    protected int targetDistance;

    protected bool isMoving;
    protected bool canAttack;
    protected bool isTargetEmpty;
    protected bool forMove;

    public bool isTurnEnd;

    private void Awake()
    {
        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
    }
    private void LateUpdate()
    {
        spriteController.SetAnimState(AnimState.Idle);
    }
    public abstract void ProceedTurn();
    public abstract void SpecialCheck();    // 고유 우선 기믹 체크
    public abstract void TargetFoundSuccess();  // 대상 발견 시 행동
    public abstract void OnHit(int damage);
    public abstract void MoveEnd();
    public void OnPathFound(Vector3[] newpath, bool succsess)
    {
        if (succsess)
        {
            path = newpath;
            isMoving = true;
            StartCoroutine(FollowPath());  // 이동 실행 후 끝날 때 까지 대기
        }
        else
        {
            Debug.Log("Move Failed");
        }
    }
    public void OnTargetFound(Vector3 newTargetPos, GameObject newTargetObj, int distance, bool succsess)
    {
        if (succsess)
        {
            targetDistance = distance;
            Debug.Log("Search Succsess");
            targetPos = newTargetPos;
            targetObj = newTargetObj;
            isTargetEmpty = false;
            if (forMove)    // 대상 이동용일 경우
            {
                TargetFoundSuccess();
            }
        }
        else
        {
            targetDistance = 999;
            if (forMove)    // 대상 이동용일 경우
            {
                Debug.Log("search Failed, Move To Random Location");
                PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
            }
        }
    }
    public void OnTurnStart()
    {
        stat.ActPoint = 100;
        isTurnEnd = false;
    }
    public void TurnEnd()
    {
        isTurnEnd = true;
        Managers.Battle.NextTurn();
    }
    public void Search(int radius, bool _forMove)
    {
        forMove = _forMove;
        PathFinder.RequestSearch(transform.position, radius, targetTag, OnTargetFound);
    }
    public void CheckAttack()
    {
        Search(stat.AttackRange, false);
        if (stat.AttackRange >= targetDistance)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
    public void Move()
    {
        if (!isMoving)
        {
            PathFinder.RequestPath(transform.position, targetPos, OnPathFound);
        }
        else
        {
            Debug.Log("Object is Moving!");
        }
    }


    public void Attack(int actPoint)
    {
        if (canAttack && stat.ActPoint >= actPoint)
        {
            Debug.Log("Attack");    // 공격 실행
            stat.ActPoint -= actPoint;
            spriteController.SetAnimState(AnimState.Attack);
            // 텀 추가?
            Managers.Active.Damage(targetObj, stat.BaseDamage);
            canAttack = false;
        }
    }

    public void OnRandomLoc(Vector3 newTargetPos)
    {
        targetPos = newTargetPos;
        isTargetEmpty = true;
        Move();
    }
    public void SetTargetTag(string tag)
    {
        targetTag = tag;
    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        Search(stat.AttackRange, false);

        if (path.Length == 0 || (path.Length <= stat.AttackRange && !isTargetEmpty))   // 이동할 필요 X
        {
            Debug.Log("Already At The Position");
            if (!isTargetEmpty)
            {
                canAttack = true;
            }
            isMoving = false;
            stat.ActPoint -= 10 * targetIndex;    // 이동 시 소모할 행동력
            yield break;
        }
        Vector3 currentWaypoint = path[targetIndex];

        if (isTargetEmpty)  // 해당 칸까지 이동
        {
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length || targetIndex + 1 > stat.MovePoint)
                    {
                        isMoving = false;
                        stat.ActPoint -= 10 * targetIndex;    // 이동 시 소모할 행동력
                        MoveEnd();
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                if (transform.position.x > currentWaypoint.x)   //현재 위치와 이동 대상 x 좌표 비교해 스프라이트 회전
                {
                    spriteController.Flip(Direction.Left);
                }
                else if (transform.position.x < currentWaypoint.x)
                {
                    spriteController.Flip(Direction.Right);
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
        else    //해당 칸 사거리 까지 이동
        {
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    if (targetIndex + stat.AttackRange + 1 >= path.Length)  // 사거리 닿을 시
                    {
                        isMoving = false;
                        canAttack = true;
                        stat.ActPoint -= 10 * targetIndex;    // 이동 시 소모할 행동력
                        MoveEnd();
                        yield break;
                    }
                    else if (targetIndex + 1 > stat.MovePoint)  // 이동거리 초과 시
                    {
                        isMoving = false;
                        stat.ActPoint -= 10 * targetIndex;    // 이동 시 소모할 행동력
                        MoveEnd();
                        yield break;
                    }
                    targetIndex++;
                    currentWaypoint = path[targetIndex];
                }

                if (transform.position.x > currentWaypoint.x)   //현재 위치와 이동 대상 x 좌표 비교해 스프라이트 회전
                {
                    spriteController.Flip(Direction.Left);
                }
                else if (transform.position.x < currentWaypoint.x)
                {
                    spriteController.Flip(Direction.Right);
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}

