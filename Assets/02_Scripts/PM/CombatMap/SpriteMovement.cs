// SpriteMovement.cs
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public float moveSpeed = 5f; // 스프라이트 이동 속도
    private Vector3 targetPosition; // 목표 위치

    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            float xMovement = mouseWorldPosition.x - transform.position.x;
            float yMovement = 0;
            float zMovement = 0;

            // y 이동량이 양수면 zMovement에 양수를, 음수면 음수를 할당
            if (mouseWorldPosition.y > transform.position.y)
            {
                zMovement = mouseWorldPosition.y - transform.position.y;
            }
            else if (mouseWorldPosition.y < transform.position.y)
            {
                zMovement = -(transform.position.y - mouseWorldPosition.y);
            }

            // y 이동량은 무시하기 위해 0으로 설정
            transform.position += new Vector3(xMovement, yMovement, zMovement) * moveSpeed * Time.deltaTime;
        }
        else if (transform.position != targetPosition) // 마우스를 따라 이동 중이 아니고, 목표 위치에 도달하지 않았을 때만 이동
        {
            // 스프라이트를 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 큐브와 충돌했을 때 목표 위치를 큐브의 원점으로 설정
        targetPosition = new Vector3(other.transform.position.x, 1f, other.transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        // 충돌이 유지되는 동안에는 목표 위치를 큐브의 원점으로 유지
        targetPosition = new Vector3(other.transform.position.x, 1f, other.transform.position.z);
    }
}
