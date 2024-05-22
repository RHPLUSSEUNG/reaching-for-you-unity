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
    private CapsuleCollider colider;
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
        colider = this.transform.GetChild(1).GetComponent<CapsuleCollider>();
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
        string objName = other.gameObject.name;
        switch(objName)
        {
            case "Camera1Zone":
                carmeraIndex = 1;
                cameraController.ChangeTarget(carmeraIndex,true);
                break;
            case "Camera2Zone":
                carmeraIndex = 2;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera3Zone":
                carmeraIndex = 3;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera4Zone":
                carmeraIndex = 4;   
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera5Zone":
                carmeraIndex = 5;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera6Zone":
                carmeraIndex = 6;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera7Zone":
                carmeraIndex = 7;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "Camera8Zone":
                carmeraIndex = 8;
                cameraController.ChangeTarget(carmeraIndex, true);
                break;
            case "ToFloor1":
                StartFadeEffect();
                spwanPointIndex = 1;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 2;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "ToFloor2":
                StartFadeEffect();
                spwanPointIndex = 2;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 4;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "ToFloor3":
                StartFadeEffect();
                spwanPointIndex = 3;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 6;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "ToFloor4":
                StartFadeEffect();
                spwanPointIndex = 4;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 7;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "ToClass1":
                StartFadeEffect();
                spwanPointIndex = 5;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 8;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "Class1ToFloor1":
                StartFadeEffect();
                spwanPointIndex = 6;
                gameObject.transform.position = spwanPoint[spwanPointIndex].position;
                carmeraIndex = 1;
                cameraController.ChangeTarget(carmeraIndex, false);
                break;
            case "ETDNPC":
                SceneManager.LoadScene("Prototype_0512");
                break;
        }   
    }

    void Move()
    {
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        facingDirectrion = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z).normalized;
        sideDirection = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z).normalized;
        float speed = Mathf.Min((facingDirectrion * inputVec.y + sideDirection * inputVec.x).magnitude, 1.0f) * moveSpeed;
        nextVec = (facingDirectrion * inputVec.y + sideDirection * inputVec.x) * speed * Time.fixedDeltaTime;
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
        rayPositions.Add(transform.position + Vector3.up * colider.radius);

        float rayScopeValue;

        if (inputVec.x > 0 && inputVec.y >0)
        {
            rayScopeValue = rayScope * (float)1.4;
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
                    return true;
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
