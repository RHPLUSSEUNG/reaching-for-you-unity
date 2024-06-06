using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField]
    float speed =10;

    private SpriteController spriteController;
    PlayerStat stat;
    Vector3[] path;
    private int targetIndex;

    bool isMoved;

    public bool isMoving;

    private void Awake()
    {
        spriteController = GetComponent<SpriteController>();
        stat = GetComponent<PlayerStat>();
        isMoved = false;
        isMoving = false;
    }

    public void Move(GameObject target)
    {
        PathFinder.RequestPath(transform.position, target.transform.position, OnPathFound);
    }

    public void OnHit(int damage)
    {
        spriteController.SetAnimState(AnimState.Hit);
        stat.Hp -= damage;
    }
    public void OnPathFound(Vector3[] newpath, bool succsess)
    {
        if (newpath.Length ==0)
        {
            return;
        }
        if (succsess)
        {
            spriteController.SetAnimState(AnimState.Move);
            path = newpath;
            isMoving = true;
            StartCoroutine(FollowPath());  // 이동 실행 후 끝날 때 까지 대기
        }
        else
        {
            Debug.Log("Move Failed");
        }
    }

    public void OnMoveEnd()
    {
        isMoving = false;
        isMoved = true;
        spriteController.SetAnimState(AnimState.Idle);
    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
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

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            if (transform.position == currentWaypoint)
            {
                if (targetIndex + 1 >= path.Length || targetIndex + 1 >= stat.MovePoint)
                {
                    stat.ActPoint -= 10 * (targetIndex + 1);    // 이동 시 소모할 행동력
                    isMoved = true;
                    OnMoveEnd();
                    yield break;
                }
                targetIndex++;
                currentWaypoint = path[targetIndex];
            }
            yield return null;
        }
    }
}
