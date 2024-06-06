using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField]
    float speed =10;

    GameObject mainCamera;
    CameraController cameraController;

    private SpriteController spriteController;
    PlayerStat stat;
    Vector3[] path;
    private int targetIndex;

    CapsuleCollider collider;

    bool isMoved;

    public bool isMoving;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();

        spriteController = GetComponent<SpriteController>();
        collider = GetComponent<CapsuleCollider>();
        stat = GetComponent<PlayerStat>();
        isMoved = false;
        isMoving = false;
    }
    private void Start()
    {
        cameraController.AddFollowList(this.gameObject);
    }

    public void StateUpdate()
    {
        if (stat.CanPassWall)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }

    public void Move(GameObject target)
    {
        if (!isMoving)
        {
            if (!PathFinder.CheckWalkable(target.transform.position))    //목표 위치가 벽일 시
            {
                if (stat.CanPassWall)   // 벽 통과 가능 시
                {

                }
                else
                {
                    Debug.Log("Can't Move There!");
                    return;
                }
            }
            PathFinder.RequestPath(transform.position, target.transform.position, OnPathFound);
        }
    }

    public void OnHit(int damage)
    {
        Debug.Log("Player OnHit");
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
        cameraController.ChangeCameraMode(CameraMode.Follow, true);
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
