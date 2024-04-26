using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTemp : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Vector3 clickPosition = Input.mousePosition;
            Debug.Log(clickPosition);
            GetMousePosition(clickPosition);
        }
    }


    public Vector3 GetMousePosition(Vector3 pos)
    {
        return pos;
    }
}
