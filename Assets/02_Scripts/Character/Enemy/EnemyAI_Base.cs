using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAI_Base : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    [SerializeField]
    string targetTag = "Player";

    protected EnemyStat stat;
    protected GameObject targetObj;
    protected SpriteController spriteController;
    protected SkillList skillList;

    List<Vector3> path;
    protected Vector3 targetPos;
    protected int targetIndex;
    protected int targetDistance;

    protected bool isMoved;
    protected bool isTargetFound;
    protected bool isAttacked;

    protected int actPoint;

    public bool isTurnEnd;

    protected GameObject cover;

    public Dictionary<string, float> actDic;   // 확률 행동 딕셔너리

    private void Start()
    {
        cover = null;

        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        //skillList.AddSkill(Managers.Skill.Instantiate(0)); 각 몬스터마다 해당하는 번호의 스킬 가져오기
    }

    public abstract void ProceedTurn(); // 턴 진행
    public abstract void SpecialCheck();  // 고유 기믹 체크
    public abstract void OnTargetFoundSuccess();  // 대상 발견 시 행동
    public abstract void OnTargetFoundFail(); // 대상 발견 실패 시 행동
    public abstract void OnPathFailed();    // 경로를 찾을 수 없을 때
    public abstract void OnHit(int damage);
    public abstract void OnMoveEnd();   // 이동이 끝났을 때
    public abstract void OnAttackSuccess(); //공격 성공 시
    public abstract void OnAttackFail();    // 공격 실패 시
    public abstract void BeforeTrunEnd();
    public abstract void RadomTile();

    public void OnTurnStart()
    {
        isMoved = false;
        isAttacked = false;
        stat.ActPoint = 100;
        isTurnEnd = false;
    }
    public void TurnEnd()
    {
        if (!isTurnEnd)
        {
            isTurnEnd = true;
            NextTurn();
        }
    }
    public void NextTurn()
    {
        Managers.Battle.NextTurn();
    }
    public void Search(int radius)
    {
         PathFinder.RequestSearch(transform.position, radius, targetTag, OnTargetFound);
    }
    public void OnTargetFound(Vector3 newTargetPos, GameObject newTargetObj, int distance, bool succsess)
    {
        if (succsess)
        {
            targetDistance = distance;
            Debug.Log("Search Success");
            targetPos = newTargetPos;
            targetObj = newTargetObj;
            isTargetFound = true;
            SetDirection();
            OnTargetFoundSuccess();
        }
        else
        {
            targetDistance = 999;
            Debug.Log("Search Failed");
            OnTargetFoundFail();
        }
    }
    public void Move()
    {
        if (!isMoved)
        {
            isMoved = true;
            PathFinder.RequestPath(transform.position, targetPos, OnPathFound);
        }
        else
        {
            Debug.Log("Object is Moving!");
        }
    }
    public void OnPathFound(List<Vector3> newpath, bool succsess)
    {
        if (succsess)
        {
            path = newpath;
            StartCoroutine(FollowPath());  // 이동 실행 후 끝날 때 까지 대기
        }
        else
        {
            Debug.Log("Move Failed");
            OnPathFailed();
        }
    }
    public bool CanAttack(int range)
    {
        return range >= targetDistance ? true : false;
    }
    public void Attack(int actPoint)
    {
        isAttacked = true;
        if (stat.ActPoint >= actPoint)
        {
            Debug.Log("Attack");    // 공격 실행
            stat.ActPoint -= actPoint;
            spriteController.SetAnimState(AnimState.Attack);
            // 텀 추가?
        }
        else
        {
            Debug.Log("Attack Failed");
            OnAttackFail();
        }
        TurnEnd();
    }

    public void AttackEvent()
    {
        Managers.Active.SetCoverData(cover);
        Managers.Active.Damage(targetObj.transform.parent.gameObject, stat.BaseDamage); //targetObj 반환값 = 콜라이더를 가지고 있는 오브젝트 > 플레이어는 하위 오브젝트에 콜라이더 존재
        OnAttackSuccess();
    }
    public void GetRandomLoc(int radius)
    {
        PathFinder.RequestRandomLoc(transform.position, radius, OnRandomLoc);
    }
    public void GetRandomLoc(int radius, bool randTile) //임시 타일 탐색
    {
        PathFinder.RequestRandomLoc(transform.position, radius, OnRandomTile);
    }
    public void OnRandomLoc(Vector3 newTargetPos)
    {
        targetPos = newTargetPos;
        isTargetFound = false;
        Move();
    }
    public void OnRandomTile(Vector3 newTargetPos)
    {
        Debug.Log(newTargetPos);
        targetPos = newTargetPos;
        isTargetFound = false;
        RadomTile();
    }
    public void SetTargetTag(string tag)
    {
        targetTag = tag;
    }
    public void SetDirection()
    {
        if (transform.position.x > targetObj.transform.position.x)   //현재 위치와 이동 대상 x 좌표 비교해 스프라이트 회전
        {
            spriteController.Flip(Direction.Left);
        }
        else if (transform.position.x < targetObj.transform.position.x)
        {
            spriteController.Flip(Direction.Right);
        }
    }
    public void OnSkillRangeFound(List<GameObject> tileList)
    {
        Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().HighlightRange(tileList);
        Invoke("OffSkillRange", 1.0f);
    }
    void OffSkillRange()
    {
        Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().ClearSkillRange(); //범위 표시 제거
    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        if (path.Count == 0 || (path.Count <= stat.AttackRange && isTargetFound))   // 이동할 필요 X
        {
            Debug.Log("Already At The Position");
            stat.ActPoint -= 10 * (targetIndex + 1);    // 이동 시 소모할 행동력
            isMoved = true;
            OnMoveEnd();
            yield break;
        }
        Vector3 currentWaypoint = path[targetIndex];

        while (true)
        {
            if (transform.position.x > currentWaypoint.x)   //현재 위치와 이동 대상 x 좌표 비교해 스프라이트 회전
            {
                spriteController.Flip(Direction.Left);
            }
            else if (transform.position.x < currentWaypoint.x)
            {
                spriteController.Flip(Direction.Right);
            }

            Vector3 moveTarget = new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z);   // y좌표 배제
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);

            if (transform.position == moveTarget)
            {
                if (!isTargetFound)  // 대상 칸까지 이동 시
                {
                    if (targetIndex + 1 >= path.Count || targetIndex + 1 >= stat.MovePoint)
                    {
                        stat.ActPoint -= 10 * (targetIndex + 1);    // 이동 시 소모할 행동력
                        isMoved = true;
                        OnMoveEnd();
                        yield break;
                    }
                }
                else
                { 
                    if (targetIndex + stat.AttackRange + 1 >= path.Count || targetIndex + 1 >= stat.MovePoint)  // 사거리 닿을 시 or 이동거리 초과 시
                    {
                        stat.ActPoint -= 10 * (targetIndex + 1);    // 이동 시 소모할 행동력
                        isMoved = true;
                        OnMoveEnd();
                        yield break;
                    }
                }
                targetIndex++;
                currentWaypoint = path[targetIndex];
            }
            yield return null;
        }
    }
    public string RandChoose(Dictionary<string, float> dic)  // 행동 이름, 확률 인자, 확률 총합 1이여야 함
    {
        float randValue = Random.Range(0f, 1f);
        float sum = 0;

        foreach (KeyValuePair<string, float> kvp in dic)
        {
            sum += kvp.Value;
        }
        if (sum != 1)   // 확률 총합 검사
            return "Chane Sum is Not 1";

        sum = 0;
        foreach (KeyValuePair<string, float> kvp in dic)
        {
            sum += kvp.Value;
            if (randValue <= sum)
                return kvp.Key;
        }
        return null;
    }
}

