using System;
using System.Collections;
using UnityEngine;

public class EnemyAI_Test : MonoBehaviour
{
    
    public EntityStat stat;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    string targetTag = "Player";

    Vector3 targetPos;
    int targetIndex;
    GameObject targetObj;
    Vector3[] path;
    bool isMoving;
    bool canAttack;
    bool isTargetEmpty;

    public bool isTurnEnd;

    public void ProceedTurn()
    {
        //시야 범위 검색 > 찾으면 이동 > 사거리 닿을 시 공격
        //못찾으면 범위 내 랜덤 위치 받은 후 이동
        isTurnEnd = false;
        canAttack = false;
        Search(stat.Sight);
    }

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
    public void OnTargetFound(Vector3 newTargetPos, GameObject newTargetObj, bool succsess)
    {
        if (succsess)
        {
            Debug.Log("Search Succsess, Move to Target");
            targetPos = newTargetPos;
            targetObj = newTargetObj;
            isTargetEmpty = false;
            Move();
        }
        else
        {
            // 서치 실패 시 몹마다 특정 로직
            // 이동 범위 안의 무작위 좌표를 대상으로 움직임
            Debug.Log("search Failed, Move To Random Location");
            PathFinder.RequestRandomLoc(transform.position, stat.MovePoint, OnRandomLoc);
        }
    }  
    public void OnRandomLoc(Vector3 newTargetPos)
    {
        Debug.Log(newTargetPos);
        targetPos = newTargetPos;
        isTargetEmpty = true;
        Move();
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
    public void Search(int radius)
    {
        PathFinder.RequestSearch(transform.position, radius, targetTag, OnTargetFound);
    }

    public void Attack()
    {
        if (canAttack)
        {
            Debug.Log("Attack");    // 공격 실행
            Managers.Party.Damage(targetObj, stat.BaseDamage);
            canAttack = false;
            
        }
        isTurnEnd = true;   // 턴 종료
        Debug.Log("Enemy Turn end");
        Managers.Battle.NextTurn();
        // 여기에 턴 종료 추가

    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        if (path.Length == 0 || (path.Length == 1 && !isTargetEmpty))   // 이미 도착
        {
            Debug.Log("Already At The Position");
            if (!isTargetEmpty)
                canAttack= true;
            isMoving = false;
            Attack();
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
                    if (targetIndex >= path.Length || targetIndex >= stat.MovePoint)
                    {
                        isMoving = false;
                        Attack();
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
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
                    if (targetIndex + stat.AttackRange +1 >= path.Length)  // 사거리 닿을 시
                    {
                        isMoving = false;
                        canAttack = true;
                        Attack();
                        yield break;
                    }
                    else if (targetIndex >= stat.MovePoint) // 이동거리 초과 시
                    {
                        isMoving = false;
                        Attack();
                        yield break;
                    }
                    targetIndex++;
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
        
    }
}

