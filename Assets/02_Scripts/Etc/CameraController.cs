using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject cameraList;
    [SerializeField]
    GameObject followList;

    Transform[] cameraTargets;
    GameObject[] followTargets;
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
            cameraIndex = 1;     // GetComponentsInChildren 사용 시 0번엔 부모 오브젝트 정보가 위치함으로 Index를 1부터
        }
        if (followList != null)
        {
            followTargets = followList.GetComponentsInChildren<GameObject>();
            followIndex = 0;
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

        // 임시 캐릭터 따라가기 설정 코드
        followTargets = GameObject.FindGameObjectsWithTag("Player");
        followIndex= 0;
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
    public void ChangeCameraMode(bool _isSmoothMove)
    {
        if (isFollowMode)
        {
            isFollowMode = false;
            targetTransform = cameraTargets[cameraIndex].transform;

        }
        else
        {
           isFollowMode = true;
            targetTransform = followTargets[followIndex].transform;
        }
        isSmoothMove = _isSmoothMove;
        Debug.Log("Changed Mode");
    }
    public void ChangePos(Transform target)
    {
        targetTransform = target;
        setPos = target.position;
    }
}