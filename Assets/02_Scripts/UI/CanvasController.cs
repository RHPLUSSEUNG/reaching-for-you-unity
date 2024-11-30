using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void ChangeCanvasOrder()
    {
        CanvasManager.Instance.ChangeCanvasOrder(canvas);
    }
}
