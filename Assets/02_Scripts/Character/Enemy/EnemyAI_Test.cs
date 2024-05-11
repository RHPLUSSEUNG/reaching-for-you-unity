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
        //�þ� ���� �˻� > ã���� �̵� > ��Ÿ� ���� �� ����
        //��ã���� ���� �� ���� ��ġ ���� �� �̵�
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
            StartCoroutine(FollowPath());  // �̵� ���� �� ���� �� ���� ���
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
            // ��ġ ���� �� ������ Ư�� ����
            // �̵� ���� ���� ������ ��ǥ�� ������� ������
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
            Debug.Log("Attack");    // ���� ����
            Managers.Party.Damage(targetObj, stat.BaseDamage);
            canAttack = false;
            
        }
        isTurnEnd = true;   // �� ����
        Debug.Log("Enemy Turn end");
        Managers.Battle.NextTurn();
        // ���⿡ �� ���� �߰�

    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        if (path.Length == 0 || (path.Length == 1 && !isTargetEmpty))   // �̹� ����
        {
            Debug.Log("Already At The Position");
            if (!isTargetEmpty)
                canAttack= true;
            isMoving = false;
            Attack();
            yield break;
        }
        Vector3 currentWaypoint = path[targetIndex];

        if (isTargetEmpty)  // �ش� ĭ���� �̵�
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
        else    //�ش� ĭ ��Ÿ� ���� �̵�
        {
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    if (targetIndex + stat.AttackRange +1 >= path.Length)  // ��Ÿ� ���� ��
                    {
                        isMoving = false;
                        canAttack = true;
                        Attack();
                        yield break;
                    }
                    else if (targetIndex >= stat.MovePoint) // �̵��Ÿ� �ʰ� ��
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

