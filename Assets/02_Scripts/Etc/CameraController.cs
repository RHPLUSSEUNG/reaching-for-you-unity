using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject cameraList;

    Transform[] cameraTargets;
    [SerializeField]
    List<GameObject> followTargets;
    [SerializeField]
    float cameraSpeed;
    [Header("Follow Mode Offset")]
    [SerializeField]
    float followOffsetX;
    [SerializeField]
    float followOffsetY;
    [SerializeField]
    float followOffsetZ;
    [SerializeField]
    float followRotateX;
    [SerializeField]
    float followRotateY;
    [Header("UI Mode Offset")]
    [SerializeField]
    float uiOffsetX;
    [SerializeField]
    float uiOffsetY;
    [SerializeField]
    float uiOffsetZ;
    [SerializeField]
    float uiRotateX;
    [SerializeField]
    float uiRotateY;

    [SerializeField]
    float screenEdge;

    float offsetX,offsetY,offsetZ,rotateX, rotateY;
    bool isFollowMode;
    bool isWaiting;

    CameraMode mode;

    private bool isSmoothMove = true;
    private Vector3 setPos;
    private Transform targetTransform;
    private int cameraIndex;
    [SerializeField]
    GameObject followTarget;

    [SerializeField]
    float inputSpeed = 10;

    float time = 1.0f;

    [SerializeField]
    GameObject worldCamera;

    private void Awake()
    {
        isWaiting =false;

        if (cameraList != null)
        {
            cameraTargets = cameraList.GetComponentsInChildren<Transform>();
            cameraIndex = 1;     // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터
        }

        if (mode == CameraMode.Follow)
        {
            FollowOffset();
            transform.eulerAngles = new Vector3(rotateX, rotateY, 0);
            ChangeFollowTarget(followTargets[0], true);
            mode = CameraMode.Follow;
        }
        else
        {
            ChangeCameraTarget(cameraIndex, false);
        }
        
    }
    void FixedUpdate()
    {
        if(isWaiting)
        {
            return;
        }

       switch(mode)
        {
            case CameraMode.Static:
                if (!isSmoothMove)
                {
                    transform.position = setPos;
                    transform.rotation = targetTransform.rotation;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * cameraSpeed);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, Time.deltaTime * cameraSpeed);
                }
                break;
            case CameraMode.UI:
            case CameraMode.Follow:

                setPos = new Vector3(
                   targetTransform.position.x + offsetX,
                   targetTransform.position.y + offsetY,
                   targetTransform.position.z + offsetZ
                   );
                if (!isSmoothMove)
                {
                    transform.position = setPos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * cameraSpeed);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotateX,rotateY,0), Time.deltaTime * cameraSpeed);
                }
                break;
            case CameraMode.Move:
                CameraInput();
                break;
        }
    }

    void LateUpdate()
    {
        if (isWaiting)
        {
            return;
        }
        if (mode == CameraMode.Follow)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance,
                                1 << LayerMask.NameToLayer("EnvironmentObject"));

            for (int i = 0; i < hits.Length; i++)
            {
                EnvironmentObject[] obj = hits[i].transform.GetComponentsInChildren<EnvironmentObject>();

                for (int j = 0; j < obj.Length; j++)
                {
                    obj[j].Transparent();
                    Debug.Log("Transparenting Object");
                }
            }
            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }
    }
    public void ChangeCameraTarget(int targetIndex, bool _isSmoothMove)
    {
        isSmoothMove = _isSmoothMove;
        targetTransform = cameraTargets[targetIndex];
        isFollowMode = false;
        ChangePos(targetTransform);
        //Debug.Log("Changed CameraTarget To " + targetIndex);

        isWaiting = false;
    }
    public void ChangeFollowTarget(GameObject target, bool _isSmoothMove)
    {
        isSmoothMove = _isSmoothMove;
        followTarget = target;
        targetTransform = followTarget.transform;
        isFollowMode = true;
        //Debug.Log("Changed FollowTarget To " + target);

        isWaiting = false;
    }
    public void ChangeCameraMode(CameraMode _mode, bool isOrthographic, bool _isSmoothMove)
    {
        switch (_mode)
        {
            case CameraMode.Static:
                targetTransform = cameraTargets[cameraIndex].transform;
                transform.eulerAngles = cameraTargets[cameraIndex].transform.eulerAngles;
                transform.position = setPos;    //1회만 설정
                break;
            case CameraMode.Follow:
                FollowOffset();
                targetTransform = followTarget.transform;
                break;
            case CameraMode.UI:
                UIOffset();
                targetTransform = followTarget.transform;
                break;
            case CameraMode.Move:
                break;
        }
        mode = _mode;
        gameObject.GetComponent<Camera>().orthographic = isOrthographic;
        isSmoothMove = _isSmoothMove;
        setPos = targetTransform.position;

    }
    public void AddFollowList(GameObject target)
    {
        followTargets.Add(target);
    }
    public void ChangePos(Transform target)
    {
        targetTransform = target;
        setPos = target.position;
    }
    public void ChangeOffSet(float x, float y, float z, float _rotateX, float _rotateY)
    {
        offsetX = x;
        offsetY = y;
        offsetZ = z;
        rotateX = _rotateX;
        rotateY = _rotateY;
    }
    public void FollowOffset()
    {
        offsetX = followOffsetX;
        offsetY = followOffsetY;
        offsetZ = followOffsetZ;
        rotateX = followRotateX;
        rotateY = followRotateY;
    }
    public void UIOffset()
    {
        offsetX = uiOffsetX;
        offsetY = uiOffsetY;
        offsetZ = uiOffsetZ;
        rotateX = uiRotateX;
        rotateY = uiRotateY;
    }
    private void CameraInput()
    {
        Vector3 vec = new Vector3();
        if (Input.GetKey(KeyCode.W)) //|| Input.mousePosition.y >= Screen.height - screenEdge
            vec += new Vector3(1f, 0, 1f);
        if (Input.GetKey(KeyCode.S)) //|| Input.mousePosition.y <= screenEdge
            vec += new Vector3(-1f, 0, -1f);
        if (Input.GetKey(KeyCode.D)) //|| Input.mousePosition.x >=  screenEdge
            vec += new Vector3(1f, 0, -1f);
        if (Input.GetKey(KeyCode.A)) //|| Input.mousePosition.x <= Screen.width - screenEdge
            vec += new Vector3(-1f, 0, 1f);

        vec.Normalize();
        worldCamera.transform.position += vec * inputSpeed * Time.deltaTime;
    }
    public void WaitForNewTarget()
    {
        isWaiting = true;
    }
}