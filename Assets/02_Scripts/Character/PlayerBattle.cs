using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    GameObject mainCamera;
    CameraController cameraController;

    SpriteController spriteController;
    PlayerStat stat;
    List<Vector3> path;
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
        stat.MovePoint = stat.MaxMovePoint;
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
                    Managers.BattleUI.warningUI.SetText("�̵��� �� �����ϴ�!");
                    Managers.BattleUI.warningUI.ShowWarningUI();
                    return;
                }
            }
            PathFinder.RequestPath(transform.position, target.transform.position, OnPathFound);
        }
    }

    public void OnHit(int damage)
    {
        stat.Hp -= damage;
        HitEvent();
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
    public void OnPathFound(List<Vector3> newpath, bool succsess)
    {
        if (newpath.Count == 0)
        {
            return;
        }
        if (succsess)
        {
            if (newpath.Count * 10 > stat.ActPoint || newpath.Count * 10 > stat.MovePoint)
            {
                Debug.Log("�ൿ�� or �̵��� ����!");
                Managers.BattleUI.warningUI.SetText("�̵��� �� �����ϴ�!");
                Managers.BattleUI.warningUI.ShowWarningUI();
                return;
            }
            Managers.BattleUI.actUI.GetComponent<MoveRangeUI>().ShowPathRange(newpath);
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
        Managers.BattleUI.actUI.GetComponent<MoveRangeUI>().ClearPathRange();
        //cameraController.ChangeCameraMode(CameraMode.Follow, true);
    }

    public void PotionUse()
    {
        spriteController.SetAnimState(AnimState.Potion);
        Invoke("OnHitEnd", 0.1f);
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
            Vector3 moveTarget = new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z);   // y��ǥ ����
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);

            if (transform.position == moveTarget)
            {
                if (targetIndex + 1 >= path.Count)
                {
                    stat.ActPoint -= 10 * (targetIndex + 1);    // �̵� �� �Ҹ��� �ൿ��
                    stat.MovePoint -= 10 * (targetIndex + 1);
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
