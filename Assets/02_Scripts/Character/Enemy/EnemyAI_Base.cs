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

    public Dictionary<string, float> actDic;   // Ȯ�� �ൿ ��ųʸ�

    private void Start()
    {
        cover = null;

        stat = GetComponent<EnemyStat>();
        spriteController = GetComponent<SpriteController>();
        isTurnEnd = true;
        skillList = GetComponent<SkillList>();
        //skillList.AddSkill(Managers.Skill.Instantiate(0)); �� ���͸��� �ش��ϴ� ��ȣ�� ��ų ��������
    }

    public abstract void ProceedTurn(); // �� ����
    public abstract void SpecialCheck();  // ���� ��� üũ
    public abstract void OnTargetFoundSuccess();  // ��� �߰� �� �ൿ
    public abstract void OnTargetFoundFail(); // ��� �߰� ���� �� �ൿ
    public abstract void OnPathFailed();    // ��θ� ã�� �� ���� ��
    public abstract void OnHit(int damage);
    public abstract void OnMoveEnd();   // �̵��� ������ ��
    public abstract void OnAttackSuccess(); //���� ���� ��
    public abstract void OnAttackFail();    // ���� ���� ��
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
            StartCoroutine(FollowPath());  // �̵� ���� �� ���� �� ���� ���
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
            Debug.Log("Attack");    // ���� ����
            stat.ActPoint -= actPoint;
            spriteController.SetAnimState(AnimState.Attack);
            // �� �߰�?
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
        Managers.Active.Damage(targetObj.transform.parent.gameObject, stat.BaseDamage); //targetObj ��ȯ�� = �ݶ��̴��� ������ �ִ� ������Ʈ > �÷��̾�� ���� ������Ʈ�� �ݶ��̴� ����
        OnAttackSuccess();
    }
    public void GetRandomLoc(int radius)
    {
        PathFinder.RequestRandomLoc(transform.position, radius, OnRandomLoc);
    }
    public void GetRandomLoc(int radius, bool randTile) //�ӽ� Ÿ�� Ž��
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
        if (transform.position.x > targetObj.transform.position.x)   //���� ��ġ�� �̵� ��� x ��ǥ ���� ��������Ʈ ȸ��
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
        Managers.BattleUI.actUI.GetComponent<SkillRangeUI>().ClearSkillRange(); //���� ǥ�� ����
    }
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        if (path.Count == 0 || (path.Count <= stat.AttackRange && isTargetFound))   // �̵��� �ʿ� X
        {
            Debug.Log("Already At The Position");
            stat.ActPoint -= 10 * (targetIndex + 1);    // �̵� �� �Ҹ��� �ൿ��
            isMoved = true;
            OnMoveEnd();
            yield break;
        }
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
                if (!isTargetFound)  // ��� ĭ���� �̵� ��
                {
                    if (targetIndex + 1 >= path.Count || targetIndex + 1 >= stat.MovePoint)
                    {
                        stat.ActPoint -= 10 * (targetIndex + 1);    // �̵� �� �Ҹ��� �ൿ��
                        isMoved = true;
                        OnMoveEnd();
                        yield break;
                    }
                }
                else
                { 
                    if (targetIndex + stat.AttackRange + 1 >= path.Count || targetIndex + 1 >= stat.MovePoint)  // ��Ÿ� ���� �� or �̵��Ÿ� �ʰ� ��
                    {
                        stat.ActPoint -= 10 * (targetIndex + 1);    // �̵� �� �Ҹ��� �ൿ��
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
    public string RandChoose(Dictionary<string, float> dic)  // �ൿ �̸�, Ȯ�� ����, Ȯ�� ���� 1�̿��� ��
    {
        float randValue = Random.Range(0f, 1f);
        float sum = 0;

        foreach (KeyValuePair<string, float> kvp in dic)
        {
            sum += kvp.Value;
        }
        if (sum != 1)   // Ȯ�� ���� �˻�
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

