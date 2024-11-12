using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicHealthUI : UI_Scene
{
    enum basicHealthUI
    {
        TimeLimit,
        LeftProhibitedArea,
        RightProhibitedArea,
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

    RectTransform characterRectTrnasform;
    RectTransform leftProhibit;
    RectTransform rightProhibit;

    float duration = 0.5f;
    float totalTime = 60f;
    float invincibilityTime = 2.0f;
    float knockbackTime = 5.0f;

    float trueAreaMin = 200f;
    float trueAreaMax = 300f;

    float moveDistance = 100f;
    float knockbackDistance = 300f;

    bool isIncreasing = true;
    bool isInvincible = false;

    public override void Init()
    {
        Bind<GameObject>(typeof(basicHealthUI));

        timeLimit = GetObject((int)basicHealthUI.TimeLimit).GetComponent<Slider>();
        _scrollbar = GetObject((int)basicHealthUI.TimingBar).GetComponent<Scrollbar>();
        _scrollRect = _scrollbar.GetComponent<RectTransform>();
        _trueAreaRect = GetObject((int)basicHealthUI.TrueArea).GetComponent<RectTransform>();

        characterRectTrnasform = GetObject((int)basicHealthUI.Character).GetComponent<RectTransform>();
        leftProhibit = GetObject((int)basicHealthUI.LeftProhibitedArea).GetComponent<RectTransform>();
        rightProhibit = GetObject((int)basicHealthUI.RightProhibitedArea).GetComponent<RectTransform>();

        SetTrueArea();
        StartCoroutine(MiniGameStart());
    }

    private void Update()
    {
        if (!isInvincible)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool check = CheckTrueArea(_scrollbar.value);
                if (check)
                {
                    SetTrueArea();
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(BlinkCharacter());
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
        StartCoroutine(KnockBackCharacter());
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

        float trueAreaPos = _trueAreaRect.anchoredPosition.x;

        float trueAreaStart = trueAreaPos - (_trueAreaRect.rect.width * _trueAreaRect.pivot.x);
        float trueAreaEnd = trueAreaStart + _trueAreaRect.rect.width;

        float imageStartValue = Mathf.Clamp01(trueAreaStart / slidingAreaWidth);
        float imageEndValue = Mathf.Clamp01(trueAreaEnd / slidingAreaWidth);

        if (value >= imageStartValue && value <= imageEndValue)
        {
            Debug.Log("MiniGame Success");
            SuccessTiming();
            return true;
        }
        else
        {
            Debug.Log("MiniGame Fail");
            FailTiming();
            return false;
        }
    }

    void SuccessTiming()
    {
        Vector2 newPos = characterRectTrnasform.anchoredPosition;
        newPos.x += moveDistance;
        if (newPos.x >= rightProhibit.position.x)
        {
            newPos.x = rightProhibit.position.x;
        }
        characterRectTrnasform.anchoredPosition = newPos;

        CheckReachProhibitedArea();
    }

    void FailTiming()
    {
        Vector2 newPos = characterRectTrnasform.anchoredPosition;
        newPos.x -= moveDistance;
        if (newPos.x <= leftProhibit.position.x)
        {
            newPos.x = leftProhibit.position.x;
        }
        characterRectTrnasform.anchoredPosition = newPos;

        CheckReachProhibitedArea();
    }

    bool CheckReachProhibitedArea()
    {
        bool leftCheck = IsOverlapping(characterRectTrnasform, leftProhibit);
        bool rightCheck = IsOverlapping(characterRectTrnasform, rightProhibit);

        if (leftCheck)
        {
            StartCoroutine(BlinkCharacter());
            SuccessTiming();
            return true;
        }
        if (rightCheck)
        {
            StartCoroutine(BlinkCharacter());
            FailTiming();
            return true;
        }
        return false;
    }

    bool IsOverlapping(RectTransform character, RectTransform prohibitArea)
    {
        Rect characterRect = GetWorldRect(character);
        Rect prohibitRect = GetWorldRect(prohibitArea);

        return characterRect.Overlaps(prohibitRect);
    }

    Rect GetWorldRect(RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        float xMin = corners[0].x;
        float xMax = corners[2].x;
        float yMin = corners[0].y;
        float yMax = corners[2].y;

        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    IEnumerator BlinkCharacter()
    {
        isInvincible = true;
        float timer = 0f;

        GameObject character = GetObject((int)basicHealthUI.Character);

        while (timer < invincibilityTime)
        {
            character.SetActive(!character.activeSelf);
            timer += Time.deltaTime;
            yield return null;
        }
        character.SetActive(true);
        isInvincible = false;
    }

    IEnumerator KnockBackCharacter()
    {
        float elapsed = 0f;

        while(elapsed < totalTime)
        {
            yield return new WaitForSeconds(knockbackTime);

            Vector2 newPos = characterRectTrnasform.anchoredPosition;
            newPos.x -= knockbackDistance;
            if (newPos.x <= leftProhibit.position.x)
            {
                newPos.x = leftProhibit.position.x;
            }
            characterRectTrnasform.anchoredPosition = newPos;
            Debug.Log($"character Rect : {newPos.x}");

            CheckReachProhibitedArea();
        }
    }
}
