using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTimingBar : MonoBehaviour
{
    [SerializeField]
    Scrollbar _scrollbar;
    [SerializeField]
    RectTransform _trueAreaRect;
    RectTransform _scrollRect;

    private void Start()
    {
        SetTrueArea();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetTrueArea();
        }
    }

    void SetTrueArea()
    {
        _scrollRect = _scrollbar.GetComponent<RectTransform>();
        float fotRangeValue = Random.Range(5f, 95f);
        float fotSize = Random.Range(20f, 100f);

        Debug.Log($"x : {_scrollRect.sizeDelta.x}");
        Debug.Log($"y : {_scrollRect.sizeDelta.y}");
        _trueAreaRect.gameObject.SetActive(true);
        _trueAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _scrollRect.sizeDelta.x, fotRangeValue / 100f), 0);
        _trueAreaRect.sizeDelta = new Vector2(fotSize, _scrollRect.sizeDelta.y);
    }
}
