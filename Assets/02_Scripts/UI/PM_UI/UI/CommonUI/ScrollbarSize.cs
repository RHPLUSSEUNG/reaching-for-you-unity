using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSize : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("BarSize", 0.1f);
    }

    void BarSize()
    {
        GetComponent<Scrollbar>().size = 0.15f;
    }

    public void KeepScrollValue()
    {
        float scroll_value = GetComponent<Scrollbar>().value;
    }
}
