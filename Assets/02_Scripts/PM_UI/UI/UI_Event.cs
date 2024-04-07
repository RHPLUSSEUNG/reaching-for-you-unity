using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Threading;
using System;

public class UI_Event : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;
    public Action<PointerEventData> OnClickHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnEnterHandler != null)
        {
            OnEnterHandler.Invoke(eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnExitHandler != null)
        {
            OnExitHandler.Invoke(eventData);
        }
    }

}
