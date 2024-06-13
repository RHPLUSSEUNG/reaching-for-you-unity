using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject cameraList;

    Transform[] cameraTargets;
    [SerializeField]
    List<GameObject> followTargets;
    [SerializeField]
    float offsetX = 0.0f;
    [SerializeField]
    float offsetY = 2.0f;
    [SerializeField]
    float offsetZ = -3.0f;
    [SerializeField]
    float rotateX = 30.0f;
    [SerializeField]
    float cameraSpeed = 10.0f;

    [SerializeField]
    bool isFollowMode;

    private bool isSmoothMove = true;
    private Vector3 setPos;
    private Transform targetTransform;
    private int cameraIndex;
    [SerializeField]
    GameObject followTarget;

    private void Awake()
    {
        if (cameraList != null)
        {
            cameraTargets = cameraList.GetComponentsInChildren<Transform>();
            cameraIndex = 1;     // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터
        }

        if (isFollowMode)
        {
            transform.eulerAngles = new Vector3(rotateX, 0, 0);
            ChangeFollowTarget(followTarget, true);
        }
        else
        {
            ChangeCameraTarget(cameraIndex, false);
        }
    }
    void FixedUpdate()
    {
        if(isFollowMode)
        {
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
        }
        else
        {
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
        transform.eulerAngles = new Vector3(rotateX, 0, 0);
        isSmoothMove = _isSmoothMove;
        followTarget = target;
        targetTransform = followTarget.transform;
        isFollowMode = true;
        Debug.Log("Changed FollowTarget To " + target);
    }
    public void ChangeCameraMode(CameraMode mode, bool isOrthographic, bool _isSmoothMove)
    {
        switch (mode)
        {
            case CameraMode.Static:
                isFollowMode = false;
                targetTransform = cameraTargets[cameraIndex].transform;
                transform.eulerAngles = cameraTargets[cameraIndex].transform.eulerAngles;
                break;
            case CameraMode.Follow:
                isFollowMode = true;
                transform.eulerAngles = new Vector3(rotateX, 0, 0);
                targetTransform = followTarget.transform;
                break;
        }
        gameObject.GetComponent<Camera>().orthographic = isOrthographic;
        isSmoothMove = _isSmoothMove;
        setPos = targetTransform.position;
        Debug.Log("Changed Mode");
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
    public void ChangeOffSet(float x, float y, float z, float _rotateX)
    {
        offsetX = x;
        offsetY = y;
        offsetZ = z;
        rotateX = _rotateX;
    }
}