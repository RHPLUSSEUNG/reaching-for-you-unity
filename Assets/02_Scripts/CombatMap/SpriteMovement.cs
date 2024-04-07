using UnityEngine;

public class SpriteDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public float moveSpeed = 5f; // 스프라이트 이동 속도

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
            float zMovement = mouseWorldPosition.y - transform.position.y; // y 이동량에 따라 z 이동량 설정
            float yMovement = 0;
            if (zMovement < 0) // y 이동량이 음수일 때 z 이동량을 음수로 설정
            {
                yMovement = zMovement;
            }
            transform.Translate(new Vector3(xMovement, 0f, yMovement) * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
