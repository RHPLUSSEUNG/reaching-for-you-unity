using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Color hoverColor = Color.gray; // 마우스 호버 시 변경할 색상
    private Color originalColor; // 초기 색상
    private new Renderer renderer; // 숨겨진 renderer를 대체

    void Start()
    {
        renderer = GetComponent<Renderer>(); // GetComponent<Renderer>()로 초기화
        originalColor = renderer.material.color; // 초기 색상 저장
    }

    void OnMouseEnter()
    {
        renderer.material.color = hoverColor; // 마우스가 오브젝트에 진입하면 색상 변경
    }

    void OnMouseExit()
    {
        renderer.material.color = originalColor; // 마우스가 오브젝트에서 나가면 초기 색상으로 변경
    }
}
