using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // 원래 색상
    private Color originalColor;
    // 호버할 때의 색상
    public Color hoverColor = Color.gray;
    // 큐브에 마우스가 올라와 있는지 여부를 나타내는 변수
    private bool isHovered = false;

    // 스크립트가 시작될 때 실행되는 함수
    void Start()
    {
        // 큐브의 초기 색상 저장
        originalColor = GetComponent<Renderer>().material.color;
    }

    // 마우스가 오브젝트 위에 올라갔을 때 실행되는 함수
    void OnMouseEnter()
    {
        // 마우스가 오브젝트 위에 올라와 있다는 상태를 true로 변경
        isHovered = true;
        // 큐브의 색상을 hoverColor로 변경
        GetComponent<Renderer>().material.color = hoverColor;
        Debug.Log(1);
    }

    // 마우스가 오브젝트에서 벗어났을 때 실행되는 함수
    void OnMouseExit()
    {
        // 마우스가 오브젝트 위에 올라와 있는 상태를 false로 변경
        isHovered = false;
        // 큐브의 색상을 원래 색상으로 변경
        GetComponent<Renderer>().material.color = originalColor;
    }
}
