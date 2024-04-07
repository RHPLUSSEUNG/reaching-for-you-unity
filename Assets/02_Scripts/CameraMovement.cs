using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 카메라 이동 속도

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(moveAmount);
    }
}
