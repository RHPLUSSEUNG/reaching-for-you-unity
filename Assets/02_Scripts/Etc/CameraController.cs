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

    private Vector3 setPos;
    private GameObject target;
    private int index;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(rotateX, 0, 0);
        index = 0;
        target = targets[index];
    }
    void FixedUpdate()
    {
        setPos = new Vector3(
            target.transform.position.x + offsetX,
            target.transform.position.y + offsetY,
            target.transform.position.z + offsetZ
            );

        transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * cameraSpeed);
    }
    void LateUpdate()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.transform.position);
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
        Debug.DrawRay(transform.position, direction* distance, Color.red);
    }


    public void ChangeTarget()
    {
        if (++index >= targets.Length)
        {
            index = 0;
        }
        target = targets[index];
        Debug.Log("Changed Target To " + index);
    }
}