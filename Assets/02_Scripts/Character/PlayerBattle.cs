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
            if (!PathFinder.CheckWalkable(target.transform.position))    //��ǥ ��ġ�� ���� ��
            {
                if (stat.CanPassWall)   // �� ��� ���� ��
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
            StartCoroutine(FollowPath());  // �̵� ���� �� ���� �� ���� ���
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
            if (transform.position.x > currentWaypoint.x)   //���� ��ġ�� �̵� ��� x ��ǥ ���� ��������Ʈ ȸ��
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
                    stat.ActPoint -= 10 * (targetIndex + 1);    // �̵� �� �Ҹ��� �ൿ��
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
