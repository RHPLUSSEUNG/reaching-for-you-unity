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
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

   private void OnTriggerEnter(Collider other) {
        Debug.Log("1");
   }
}
