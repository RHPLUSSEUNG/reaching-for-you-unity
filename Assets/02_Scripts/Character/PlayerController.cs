using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    float basicSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 8.0f;
    [SerializeField]
    GameObject fadeEffectImg;
    [SerializeField]
    GameObject spwanPointList;

    [SerializeField]
    float rayScopeX = 1.0f;

    [SerializeField]
    float rayScopeY = 0.8f;

    [SerializeField]
    float maxSlopeAngle = 60;

    CameraController cameraController;

    Transform[] spwanPoint;
    GameObject mainCamera;
    private int carmeraIndex;
    private int spwanPointIndex;

    private SpriteController spriteController;
    private CapsuleCollider colider;
    private FadeEffect fadeEffect;

    private bool isActive;
    private float moveSpeed;
    private Vector3 inputVec;
    private Vector3 gravity;

    //private int WallLayer;
    private int groundLayer;

    Vector3 facingDirectrion;
    Vector3 sideDirection;
    Vector3 nextVec;

    private RaycastHit slopeHit;


    void Awake()
    {
        //WallLayer = 1 << LayerMask.NameToLayer("Wall");
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        spwanPoint = spwanPointList.GetComponentsInChildren<Transform>();
        spwanPointIndex = 1;    // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터

        rigid = GetComponent<Rigidbody>();

        colider = this.transform.GetChild(2).GetComponent<CapsuleCollider>();
        moveSpeed = basicSpeed;
        spriteController = GetComponent<SpriteController>();
        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraController>();
        fadeEffect = fadeEffectImg.GetComponent<FadeEffect>();

        isActive = true;
    }
    void Update()
    {
        if(!isActive)
        {
            spriteController.SetAnimState(AnimState.Idle);
            return;
        }
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
    private void FixedUpdate()
    {
        if (isActive)
        {
            Move();
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
    }
    private void LateUpdate()
    {
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)  //카메라 변경
    {
        string objTag = other.gameObject.tag;
        switch(objTag)
        {
            case "Cameras":
                carmeraIndex = other.GetComponent<CameraZone>().cameraIndex;
                cameraController.ChangeCameraTarget(carmeraIndex, true);
                break;
            case "Teleport":
                StartFadeEffect();
                spwanPointIndex = other.GetComponent<CameraZone>().spwanIndex; ;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = other.GetComponent<CameraZone>().cameraIndex;
                cameraController.ChangeCameraTarget(carmeraIndex, false);
                break;
        }   
    }

    void Move()
    {
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        facingDirectrion = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z);
        sideDirection = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z);
        nextVec = (facingDirectrion * inputVec.y + sideDirection * inputVec.x).normalized;
       
        if (CheckSlope())
        {
            nextVec = AdjustDirectionToSlope(nextVec);
            gravity = Vector3.down * Mathf.Abs(rigid.velocity.y);
        }
        else
        {
            gravity = Vector3.down;
        }
        nextVec = nextVec * moveSpeed * Time.fixedDeltaTime + gravity;

        rigid.velocity = nextVec;
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
        else if (inputVec.y != 0)
        {
            spriteController.SetAnimState(AnimState.Move);
        }
        else
        {
            spriteController.SetAnimState(AnimState.Idle);
        }
    }

    //bool CheckHitWall(Vector3 movement)
    //{
    //    movement = transform.TransformDirection(movement);

    //    List<Vector3> rayPositions = new List<Vector3>();
    //    rayPositions.Add(transform.position);
    //    rayPositions.Add(transform.position + Vector3.up * 0.5f);

    //    float rayScopeValue;

    //    if (inputVec.x > 0 && inputVec.y >0)
    //    {
    //        rayScopeValue = rayScopeX * (float)1.41;
    //    }
    //    else
    //    {
    //        rayScopeValue = rayScopeX;
    //    }

    //    foreach (Vector3 pos in rayPositions)
    //    {
    //        Debug.DrawRay(pos, movement* rayScopeValue, Color.red);
    //    }

    //    foreach (Vector3 pos in rayPositions)
    //    {
    //        if (Physics.Raycast(pos, movement, out RaycastHit hit, rayScopeValue))
    //        {
    //            if (hit.collider.CompareTag("Wall"))
    //            {
    //                return true;
    //            }    
    //        }
    //    }
    //    return false;
    //}
    public bool CheckSlope()
    {
        Ray rayPosition = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * rayScopeY, Color.red);
        if (Physics.Raycast(rayPosition, out slopeHit, rayScopeY, groundLayer))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.Log("Slope Detected");
            return angle != 0f && angle < maxSlopeAngle;
        }
        return false;
    }
    protected Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public void ChangeActive(bool active)
    {
        isActive = active;
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
    private void StartFadeEffect()
    {
        fadeEffect.StartFadeEffect();
    }
    //public void Roll()
    //{
    //    isActive = false;
    //    spriteController.SetAnimState(AnimState.Roll);
    //}
}
