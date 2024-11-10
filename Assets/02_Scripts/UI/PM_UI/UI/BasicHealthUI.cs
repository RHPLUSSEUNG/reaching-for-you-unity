using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BasicHealthUI : UI_Scene
{
    enum basicHealthUI
    {
        TimeLimit,
        Character,
        TimingBar,
        TrueArea
    }

    [SerializeField]
    Slider timeLimit;
    [SerializeField]
    Scrollbar _scrollbar;
    [SerializeField]
    RectTransform _trueAreaRect;
    RectTransform _scrollRect;

    float duration = 2.0f;
    float totalTime = 60f;

    float trueAreaMin = 200f;
    float trueAreaMax = 300f;
    bool isIncreasing = true;

    public override void Init()
    {
        Bind<GameObject>(typeof(basicHealthUI));

        timeLimit = GetObject((int)basicHealthUI.TimeLimit).GetComponent<Slider>();
        _scrollbar = GetObject((int)basicHealthUI.TimingBar).GetComponent<Scrollbar>();
        _scrollRect = _scrollbar.GetComponent<RectTransform>();
        _trueAreaRect = GetObject((int)basicHealthUI.TrueArea).GetComponent<RectTransform>();

        SetTrueArea();
        StartCoroutine(MiniGameStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool check = CheckTrueArea(_scrollbar.value);
            if(check)
            {
                SetTrueArea();
            }
        }
    }

    void SetTrueArea()
    {
        float fotRangeValue = Random.Range(10f, 90f);
        float fotSize = Random.Range(trueAreaMin, trueAreaMax);

        _trueAreaRect.gameObject.SetActive(true);
        _trueAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _scrollRect.sizeDelta.x, fotRangeValue / 100f), 0);
        _trueAreaRect.sizeDelta = new Vector2(fotSize, _scrollRect.sizeDelta.y);
    }

    IEnumerator MiniGameStart()
    {
        float elapsed = 0f;

        StartCoroutine(TimeLimitStart());
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

    IEnumerator TimeLimitStart()
    {
        float elapsed = 0f;

        while (elapsed < totalTime)
        {
            timeLimit.value = Mathf.Lerp(1, 0, elapsed / totalTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    bool CheckTrueArea(float value)
    {
        RectTransform slidingArea = _scrollbar.GetComponent<RectTransform>();
        float slidingAreaWidth = slidingArea.rect.width;

        float trueAreaLocalX = _trueAreaRect.anchoredPosition.x;

        float trueAreaStartX = trueAreaLocalX - (_trueAreaRect.rect.width * _trueAreaRect.pivot.x);
        float trueAreaEndX = trueAreaStartX + _trueAreaRect.rect.width;

        float imageStartValue = Mathf.Clamp01(trueAreaStartX / slidingAreaWidth);
        float imageEndValue = Mathf.Clamp01(trueAreaEndX / slidingAreaWidth);

        if(value >= imageStartValue && value <= imageEndValue)
        {
            Debug.Log("Success");
            SuccessTiming();
            return true;
        }
        else
        {
            Debug.Log("Fail");
            FailTiming();
            return false;
        }
    }

    void SuccessTiming()
    {
        GameObject character = GetObject((int)basicHealthUI.Character);
        RectTransform charcterTransform = character.GetComponent<RectTransform>();

        Vector2 newPos = charcterTransform.anchoredPosition;
        newPos.x += 200;
        charcterTransform.anchoredPosition = newPos;
    }

    void FailTiming()
    {
        GameObject character = GetObject((int)basicHealthUI.Character);
        RectTransform charcterTransform = character.GetComponent<RectTransform>();

        Vector2 newPos = charcterTransform.anchoredPosition;
        newPos.x -= 200;
        charcterTransform.anchoredPosition = newPos;
    }
}
