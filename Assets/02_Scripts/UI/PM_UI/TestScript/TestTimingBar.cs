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

    float duration = 1.0f;
    float totalTime = 60f;

    float trueAreaMin = 200f;
    float trueAreaMax = 300f;
    bool isIncreasing = true;

    private void Start()
    {
        SetTrueArea();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTrueArea();
            StartCoroutine(ScrollBarAnim());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetTrueArea();
        }
    }

    void SetTrueArea()
    {
        _scrollRect = _scrollbar.GetComponent<RectTransform>();
        float fotRangeValue = Random.Range(5f, 95f);
        float fotSize = Random.Range(trueAreaMin, trueAreaMax);

        _trueAreaRect.gameObject.SetActive(true);
        _trueAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _scrollRect.sizeDelta.x, fotRangeValue / 100f), 0);
        _trueAreaRect.sizeDelta = new Vector2(fotSize, _scrollRect.sizeDelta.y);
    }

    IEnumerator ScrollBarAnim()
    {
        float elapsed = 0f;

        while (elapsed < totalTime)
        {
            float timer = 0f;

            if (isIncreasing)
            {
                while (timer < duration)
                {
                    _scrollbar.value = Mathf.Lerp(0, 1, timer / duration);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (timer < duration)
                {
                    _scrollbar.value = Mathf.Lerp(1, 0, timer / duration);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    yield return null;
                }
            }

            isIncreasing = !isIncreasing;
        }
    }
}
    