using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject Target;

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

    private Vector3 targetPos;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(rotateX, 0, 0);
    }
    void FixedUpdate()
    {
        targetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * cameraSpeed);
    }
}