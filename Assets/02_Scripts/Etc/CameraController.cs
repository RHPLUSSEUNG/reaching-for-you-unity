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
    float basicOffsetX;
    [SerializeField]
    float basicOffsetY;
    [SerializeField]
    float basicOffsetZ;
    [SerializeField]
    float basicRotateX ;
    [SerializeField]
    float basicRotateY;
    [SerializeField]
    float cameraSpeed;

    float offsetX,offsetY,offsetZ,rotateX, rotateY;
    [SerializeField]
    bool isFollowMode;

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

    private void Awake()
    {
        BasicOffset();
        if (cameraList != null)
        {
            cameraTargets = cameraList.GetComponentsInChildren<Transform>();
            cameraIndex = 1;     // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터
        }

        if (isFollowMode)
        {
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
                }
                break;
            case CameraMode.Move:
                CameraInput();
                break;
        }
    }

    void LateUpdate()
    {
        if (isFollowMode)
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
        Debug.Log("Changed CameraTarget To " + targetIndex);
    }
    public void ChangeFollowTarget(GameObject target, bool _isSmoothMove)
    {
        transform.eulerAngles = new Vector3(rotateX, rotateY, 0);
        isSmoothMove = _isSmoothMove;
        followTarget = target;
        targetTransform = followTarget.transform;
        isFollowMode = true;
        Debug.Log("Changed FollowTarget To " + target);
    }
    public void ChangeCameraMode(CameraMode _mode, bool isOrthographic, bool _isSmoothMove)
    {
        switch (_mode)
        {
            case CameraMode.Static:
                mode = CameraMode.Static;
                isFollowMode = false;
                targetTransform = cameraTargets[cameraIndex].transform;
                transform.eulerAngles = cameraTargets[cameraIndex].transform.eulerAngles;
                break;
            case CameraMode.Follow:
                mode = CameraMode.Follow;
                isFollowMode = true;
                transform.eulerAngles = new Vector3(rotateX, rotateY, 0);
                targetTransform = followTarget.transform;
                break;
            case CameraMode.Move:
                mode = CameraMode.Move;
                isFollowMode = false;
                targetTransform = cameraTargets[1].transform;
                transform.eulerAngles = cameraTargets[1].transform.eulerAngles;
                break;
        }
        gameObject.GetComponent<Camera>().orthographic = isOrthographic;
        isSmoothMove = _isSmoothMove;
        setPos = targetTransform.position;
        Debug.Log("Changed Mode");

        transform.position = setPos;    //1회만 설정
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
    public void BasicOffset()
    {
        offsetX = basicOffsetX;
        offsetY = basicOffsetY;
        offsetZ = basicOffsetZ;
        rotateX = basicRotateX;
        rotateY = basicRotateY;
    }
    private void CameraInput()
    {
        Vector3 vec = new Vector3();
        if (Input.GetKey(KeyCode.W))
            vec += new Vector3(0, 1f, 0);
        if (Input.GetKey(KeyCode.S))
            vec += new Vector3(0, -1f, 0);
        if (Input.GetKey(KeyCode.A))
            vec += new Vector3(-1f, 0, 0);
        if (Input.GetKey(KeyCode.D))
            vec += new Vector3(1f, 0, 0);

        Vector3 pos = vec;
        if (pos.sqrMagnitude > 0)
        {
            time += Time.deltaTime;
            pos = pos * time * 1.0f;

            pos.x = Mathf.Clamp(pos.x, -inputSpeed, inputSpeed);
            pos.y = Mathf.Clamp(pos.y, -inputSpeed, inputSpeed);
            pos.z = Mathf.Clamp(pos.z, -inputSpeed, inputSpeed);

            transform.Translate(pos);
        }
    }
}