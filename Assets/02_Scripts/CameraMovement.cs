using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 카메라 이동 속도
    public float rotationSpeed = 2f; // 카메라 회전 속도
    private bool isRightMouseButtonPressed = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 수직 입력이 들어오면 y축으로 움직이지 않도록 y값을 0으로 설정
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 이동량 계산
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // 현재 위치에 이동량을 더하지 않고, 새로운 위치를 계산하여 적용
        transform.position += new Vector3(moveAmount.x, 0f, moveAmount.z);


        // 마우스 오른쪽 버튼 누를 때
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonPressed = true;
            lastMousePosition = Input.mousePosition;
        }
        // 마우스 오른쪽 버튼 뗄 때
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonPressed = false;
        }

        // 마우스 오른쪽 버튼을 누른 상태일 때
        if (isRightMouseButtonPressed)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            float rotationX = (-mouseDelta.y) * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, rotationX);
            lastMousePosition = Input.mousePosition;
        }
    }
}
