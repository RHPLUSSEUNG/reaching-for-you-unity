using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    float basicSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 8.0f;

    private SpriteController spriteController;
    private Transform colider;
    private bool isActive;
    private float moveSpeed;
    private Vector3 inputVec;

    void Awake()
    {
        isActive = true;
        rigid = GetComponent<Rigidbody>();
        colider = this.transform.GetChild(1);
        moveSpeed = basicSpeed;
        spriteController = GetComponent<SpriteController>();
    }
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = basicSpeed;
            }
            //if(Input.GetKey(KeyCode.Space))
            //{
            //    Roll();
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Inventory();
            }
        }
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            Move();
        }
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
            spriteController.Flip(Direction.Right);
            spriteController.SetAnimState(AnimState.Move);
        }
        else if (inputVec.x < 0)
        {
            spriteController.Flip(Direction.Left);
            spriteController.SetAnimState(AnimState.Move);
        }
        else if (inputVec.z != 0)
        {
            spriteController.SetAnimState(AnimState.Move);
        }
        else
        {
            spriteController.SetAnimState(AnimState.Idle);
        }
    }

    public void ChangeActive()
    {
        if (isActive)
        {
            isActive = false;
            Debug.Log("InActive");
        }
        else
        {
            isActive = true;
            Debug.Log("Active");
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
    public void Hit()
    {
        isActive = false;
        spriteController.SetAnimState(AnimState.Hit);
    }

    //public void Roll()
    //{
    //    isActive = false;
    //    spriteController.SetAnimState(AnimState.Roll);
    //}
}
