using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    float basicSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 8.0f;
    [SerializeField]
    GameObject[] spwanPoint;

    GameObject mainCamera;
    private int carmeraIndex;
    private int spwanPointIndex;

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
        mainCamera = GameObject.Find("Main Camera");
        spwanPointIndex = 0;
    }
    void Update()
    {
        if(!isActive)
        {
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
            case "Camera0Zone":
                carmeraIndex = 0;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera1Zone":
                carmeraIndex = 1;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera2Zone":
                carmeraIndex = 2;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera3Zone":
                carmeraIndex = 3;   
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera4Zone":
                carmeraIndex = 4;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera5Zone":
                carmeraIndex = 5;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera6Zone":
                carmeraIndex = 6;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "Camera7Zone":
                carmeraIndex = 7;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "ToFloor1":
                spwanPointIndex = 0;
                gameObject.transform.position = spwanPoint[spwanPointIndex].transform.position;
                carmeraIndex = 0;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "ToFloor2":
                spwanPointIndex = 1;
                gameObject.transform.position = spwanPoint[spwanPointIndex].transform.position;
                carmeraIndex = 2;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "ToFloor3":
                spwanPointIndex = 2;
                gameObject.transform.position = spwanPoint[spwanPointIndex].transform.position;
                carmeraIndex = 4;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "ToFloor4":
                spwanPointIndex = 3;
                gameObject.transform.position = spwanPoint[spwanPointIndex].transform.position;
                carmeraIndex = 6;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
            case "ToClass1":
                spwanPointIndex = 4;
                gameObject.transform.position = spwanPoint[spwanPointIndex].transform.position;
                carmeraIndex = 7;
                mainCamera.GetComponent<CameraController>().ChangeTarget(carmeraIndex);
                break;
        }   
    }

    void Move()
    {
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 facingDirectrion = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z).normalized;
        Vector3 sideDirection = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z).normalized;
        Vector3 nextVec = (facingDirectrion * inputVec.y + sideDirection * inputVec.x) * moveSpeed * Time.fixedDeltaTime;
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
