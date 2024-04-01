using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float rotateSpeed = 5.0f;

    private Animator animator;
    private Transform character;
    private Transform colider;
    
    private Vector3 inputVec;

    enum State
    {
        Idle = 1,
        Move = 2
    }
    enum Direction
    {
        Left = 0,
        Right = 180
    }
    enum xRotate
    {
        Left = 30,
        Right = -30
    }

    void Awake()
    {
        character = this.transform.GetChild(0);
        rigid = GetComponent<Rigidbody>();
        animator = character.GetComponent<Animator>();
        colider = this.transform.GetChild(1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory();
        }
    }
    private void FixedUpdate()
    {
       Move();
    }
    private void LateUpdate()
    {
        UpdateState();
    }

    void Move()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        Vector3 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void UpdateState()
    {
        if (inputVec.x > 0)
        {
            Flip(Direction.Right);
            this.animator.SetInteger("State", (int)State.Move);
        }
        else if (inputVec.x < 0)
        {
            Flip(Direction.Left);
            this.animator.SetInteger("State", (int)State.Move);
        }
        else if (inputVec.z != 0)
        {
            this.animator.SetInteger("State", (int)State.Move);
        }
        else
        {
            this.animator.SetInteger("State", (int)State.Idle);
        }
    }

    void Flip(Direction direction)
    {
        if (direction == Direction.Left)
        {
            StartCoroutine(RotateTo((float)Direction.Left));
        }
        else if (direction == Direction.Right)
        {
            StartCoroutine(RotateTo((float)Direction.Right));
        }
    }
    IEnumerator RotateTo(float targetAngleY)
    {
        float startAngleX = character.transform.eulerAngles.x;
        float startAngleY = character.transform.eulerAngles.y;
        float t = 0f;
        float speed = 0f;
        float targetAngleX;

        if (targetAngleY == (float)Direction.Left)
        {
            targetAngleX = (float)xRotate.Left;
        }
        else
        {
            targetAngleX = (float)xRotate.Right;
        }

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float xRotation = Mathf.LerpAngle(startAngleX, targetAngleX, speed);
            float yRotation = Mathf.LerpAngle(startAngleY, targetAngleY, speed);

            character.transform.eulerAngles = new Vector3(xRotation, yRotation, character.transform.eulerAngles.z);

            yield return null;
        }
    }
    
private void Interact()
    {
        Debug.Log("Interact");
    }
    private void Inventory()
    {
        Debug.Log("Open Inventory");
    }
}
