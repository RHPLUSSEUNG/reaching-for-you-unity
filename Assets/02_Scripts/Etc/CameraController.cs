using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject targetList;

    Transform[] targets;
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
    private int targetIndex;

    private void Awake()
    {
        targets = targetList.GetComponentsInChildren<Transform>();
        targetIndex = 1;     // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터

        if (isFollowMode )
        {
            ChangeMode(CameraMode.Follow);
            transform.eulerAngles = new Vector3(rotateX, 0, 0);
            targetTransform = targets[targetIndex];
        }
        else
        {
            ChangeMode(CameraMode.Static);
        }
        ChangeTarget(targetIndex, false);
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
        }
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
                }
            }
            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }
    }
    public void ChangeMode(CameraMode mode)
    {
        if (mode == CameraMode.Follow)
        {
            isFollowMode = true;
        }
        else if (mode == CameraMode.Static)
        {
            isFollowMode = false;
        }
    }
    public void ChangeTarget(int targetIndex, bool _isSmoothMove)
    {
        isSmoothMove = _isSmoothMove;
        targetTransform = targets[targetIndex];
        ChangePos(targetTransform);
        Debug.Log("Changed Target To " + targetIndex);
    }
    public void ChangePos(Transform target)
    {
        targetTransform = target;
        setPos = target.position;
        Debug.Log("Changed pos To " + setPos);
    }
}