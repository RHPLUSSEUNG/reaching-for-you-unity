using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField]
    float speed =10;

    GameObject mainCamera;
    CameraController cameraController;

    SpriteController spriteController;
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

    public void OnTurnStart()
    {
        stat.ActPoint = 100;
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
        stat.Hp -= damage;
        Invoke("HitEvent", 0.8f);
    }
    public void HitEvent()
    {
        spriteController.SetAnimState(AnimState.Hit);
        Invoke("OnHitEnd", 0.1f);
    }
    public void OnHitEnd()
    {
        spriteController.SetAnimState(AnimState.Idle);
    }
    public void OnPathFound(Vector3[] newpath, bool succsess)
    {
        if (newpath.Length ==0)
        {
            return;
        }
        if (succsess)
        {
            if (newpath.Length > stat.ActPoint * 10) 
            {
                Debug.Log("�ൿ�� ����!");
                return;
            }
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
        //cameraController.ChangeCameraMode(CameraMode.Follow, true);
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
