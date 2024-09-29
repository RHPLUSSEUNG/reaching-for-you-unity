using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class GimmickForUI
{
    public string name; // 기믹 이름

    [TextArea]
    public string description; // 기믹 설명
}

public class GimmickUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GimmickForUI gimmickForUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // UI 켜기
        Debug.Log(gimmickForUI.name + " + " + gimmickForUI.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // UI 끄기
        Debug.Log("UI 끄기");
    }
}
