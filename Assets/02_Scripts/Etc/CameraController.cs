using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject[] targets;

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

    private Vector3 setPos;
    private Transform targetTransform;
    private int index;

    private void Awake()
    {
        
        if (isFollowMode )
        {
            ChangeMode(CameraMode.Follow);
            transform.eulerAngles = new Vector3(rotateX, 0, 0);
            index = 0;
            targetTransform = targets[index].transform;
        }
        else
        {
            ChangeMode(CameraMode.Static);
        }
        ChangeTarget(0);
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
        transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, Time.deltaTime * cameraSpeed);
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
    public void ChangeTarget(int targetIndex)
    {
        targetTransform = targets[targetIndex].transform;
        ChangePos(targetTransform);
        Debug.Log("Changed Target To " + index);
    }
    public void ChangePos(Transform target)
    {
        targetTransform = target;
        setPos = target.position;
        Debug.Log("Changed pos To " + setPos);
    }
}