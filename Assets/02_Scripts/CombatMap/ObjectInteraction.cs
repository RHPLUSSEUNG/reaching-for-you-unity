using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Color hoverColor = Color.gray; // 마우스 호버 시 변경할 색상
    private Color originalColor; // 초기 색상
    private new Renderer renderer; // Renderer 컴포넌트

    void Start()
    {
        renderer = GetComponent<Renderer>(); // Renderer 컴포넌트 가져오기
        originalColor = renderer.material.color; // 초기 색상 저장
    }

    void OnTriggerEnter(Collider other)
    {
        renderer.material.color = hoverColor; // 색상 변경
    }

    void OnTriggerExit(Collider other)
    {
        renderer.material.color = originalColor; // 초기 색상으로 변경
    }
}
