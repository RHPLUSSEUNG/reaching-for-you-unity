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
    float rayScope=1.0f;

    CameraController cameraController;

    Transform[] spwanPoint;
    GameObject mainCamera;
    private int carmeraIndex;
    private int spwanPointIndex;

    private SpriteController spriteController;
    private BoxCollider colider;
    private FadeEffect fadeEffect;

    private bool isActive;
    private float moveSpeed;
    private Vector3 inputVec;

    Vector3 facingDirectrion;
    Vector3 sideDirection;
    Vector3 nextVec;


    void Awake()
    {
        spwanPoint = spwanPointList.GetComponentsInChildren<Transform>();
        spwanPointIndex = 1;    // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터

        rigid = GetComponent<Rigidbody>();
        colider = this.transform.GetChild(1).GetComponent<BoxCollider>();
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
    }
    private void LateUpdate()
    {
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)  //임시 카메라 변경
    {
        Debug.Log("collider Enter");
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

        facingDirectrion = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z).normalized;
        sideDirection = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z).normalized;
        nextVec = (facingDirectrion * inputVec.y + sideDirection * inputVec.x) * moveSpeed * Time.fixedDeltaTime;

        if (CheckHitWall(nextVec))
        {
            nextVec = Vector3.zero;
        }
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
        else if (inputVec.y != 0)
        {
            spriteController.SetAnimState(AnimState.Move);
        }
        else
        {
            spriteController.SetAnimState(AnimState.Idle);
        }
    }

    bool CheckHitWall(Vector3 movement)
    {
        movement = transform.TransformDirection(movement);

        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position);
        rayPositions.Add(transform.position + Vector3.up * 0.5f);

        float rayScopeValue;

        if (inputVec.x > 0 && inputVec.y >0)
        {
            rayScopeValue = rayScope * (float)1.41;
        }
        else
        {
            rayScopeValue = rayScope;
        }

        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement, Color.red);
        }

        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, rayScopeValue))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    return true;
                }    
            }
        }
        return false;
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
