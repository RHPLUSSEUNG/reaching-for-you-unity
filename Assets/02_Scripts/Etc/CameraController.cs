using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    private int followIndex;

    private void Awake()
    {
        if (cameraList != null)
        {
            cameraTargets = cameraList.GetComponentsInChildren<Transform>();
            cameraIndex = 1;     // GetComponentsInChildren ��� �� 0���� �θ� ������Ʈ ������ ��ġ������ Index�� 1����
        }

        if (isFollowMode)
        {
            transform.eulerAngles = new Vector3(rotateX, 0, 0);
            ChangeFollowTarget(followIndex, true);
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
    public void ChangeFollowTarget(int targetIndex, bool _isSmoothMove)
    {
        transform.eulerAngles = new Vector3(rotateX, 0, 0);
        isSmoothMove = _isSmoothMove;
        targetTransform = followTargets[targetIndex].transform;
        isFollowMode = true;
        Debug.Log("Changed FollowTarget To " + targetIndex);
    }
    public void ChangeCameraMode(CameraMode mode, bool _isSmoothMove)
    {
        switch(mode)
        {
            case CameraMode.Static:
                isFollowMode = false;
                targetTransform = cameraTargets[cameraIndex].transform;
                break;
            case CameraMode.Follow:
                isFollowMode = true;
                transform.eulerAngles = new Vector3(rotateX, 0, 0);
                targetTransform = followTargets[followIndex].transform;
                break;
        }
        isSmoothMove = _isSmoothMove;
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
}