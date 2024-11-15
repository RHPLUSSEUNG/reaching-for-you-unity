using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicHealthUI : UI_Scene
{
    enum basicHealthUI
    {
        TimeLimit,
        ClockCenter,
        LeftProhibitedArea,
        RightProhibitedArea,
        Character,
        TimingBar,
        FailArea,
        TrueArea,
        PerfectArea,
        Handle,
        RankBar,
        StageNumberText
    }

    [SerializeField]
    Slider timeLimit;
    [SerializeField]
    Scrollbar _scrollbar;
    RectTransform _failAreaRect;
    [SerializeField]
    RectTransform _trueAreaRect;
    [SerializeField]
    RectTransform _perfectAreaRect;
    [SerializeField]
    GameObject handle;
    [SerializeField]
    RectTransform rankBar;
    RectTransform _scrollRect;
    [SerializeField]
    Sprite perfectSprite;
    [SerializeField]
    Sprite greatSprite;
    [SerializeField]
    Sprite missSprite;

    RectTransform characterRectTrnasform;
    RectTransform leftProhibit;
    RectTransform rightProhibit;
    RectTransform clockCenter;

    TextMeshProUGUI stageNumberText;

    int stageNumber = 3;
    float knockbackTimeOffset = 1.0f;
    float speedOffset = 0.4f;
    float knockbackDistanceOffset = 50f;

    float totalTime = 20f;
    float baseSpeed = 3.0f;
    float baseknockbackTime = 8.0f;
    float baseknockbackDistance = 100f;
    float moveDistance = 100f;
    float invincibilityTime = 1.0f;

    float speed;
    float knockbackTime;
    float knockbackDistance;

    float trueAreaMin = 200f;
    float trueAreaMax = 300f;

    float perfectAreaMin = 50f;
    float perfectAreaMax = 100f;


    bool isIncreasing = true;
    bool isInvincible = false;

    public override void Init()
    {
        Bind<GameObject>(typeof(basicHealthUI));

        timeLimit = GetObject((int)basicHealthUI.TimeLimit).GetComponent<Slider>();
        _scrollbar = GetObject((int)basicHealthUI.TimingBar).GetComponent<Scrollbar>();
        _scrollRect = _scrollbar.GetComponent<RectTransform>();
        _failAreaRect = GetObject((int)basicHealthUI.FailArea).GetComponent<RectTransform>();
        _trueAreaRect = GetObject((int)basicHealthUI.TrueArea).GetComponent<RectTransform>();
        _perfectAreaRect = GetObject((int)basicHealthUI.PerfectArea).GetComponent<RectTransform>();
        rankBar = GetObject((int)basicHealthUI.RankBar).GetComponent<RectTransform>();
        handle = GetObject((int)basicHealthUI.Handle);
        stageNumberText = GetObject((int)basicHealthUI.StageNumberText).GetComponent<TextMeshProUGUI>();
        clockCenter = GetObject((int)basicHealthUI.ClockCenter).GetComponent<RectTransform>();

        characterRectTrnasform = GetObject((int)basicHealthUI.Character).GetComponent<RectTransform>();
        leftProhibit = GetObject((int)basicHealthUI.LeftProhibitedArea).GetComponent<RectTransform>();
        rightProhibit = GetObject((int)basicHealthUI.RightProhibitedArea).GetComponent<RectTransform>();

        SetLevel();
        SetTrueArea();
        StartCoroutine(MiniGameStart());
        Debug.Log($"Stage {stageNumber}!");
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
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            stageNumber++;
            SetLevel();
            SetTrueArea();
            StartCoroutine(MiniGameStart());
        }
    }

    public void SetLevel()
    {
        speed = baseSpeed - (speedOffset * stageNumber);
        Debug.Log($"Speed : {speed}");
        knockbackTime = baseknockbackTime - (knockbackTimeOffset * stageNumber);
        Debug.Log($"knockbackTime : {knockbackTime}");
        knockbackDistance = baseknockbackDistance + (knockbackDistanceOffset * stageNumber);
        Debug.Log($"knockbackDistance : {knockbackDistance}");

        stageNumberText.text = $"Stage : {stageNumber}";
    }

    void SetTrueArea()
    {
        float rangeValue = Random.Range(5f, 95f);
        float trueSize = Random.Range(trueAreaMin, trueAreaMax);
        float perfectSize = Random.Range(perfectAreaMin, perfectAreaMax);

        _trueAreaRect.gameObject.SetActive(true);
        _trueAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _failAreaRect.sizeDelta.x, rangeValue / 100f), 0);
        _perfectAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _failAreaRect.sizeDelta.x, rangeValue / 100f), 0);
        _trueAreaRect.sizeDelta = new Vector2(trueSize, _failAreaRect.sizeDelta.y);
        _perfectAreaRect.sizeDelta = new Vector2(perfectSize, _failAreaRect.sizeDelta.y);
    }

    IEnumerator MiniGameStart()
    {
        float elapsed = 0f;

        StartCoroutine(TimeLimitStart());
        StartCoroutine(ClockRotate());
        StartCoroutine(KnockBackCharacter());
        while (elapsed < totalTime)
        {
            float timer = 0f;

            if (isIncreasing)
            {
                while (timer < speed)
                {
                    _scrollbar.value = Mathf.Lerp(0, 1, timer / speed);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    if (elapsed >= totalTime)
                    {
                        yield break;
                    }
                    yield return null;
                }
            }
            else
            {
                while (timer < speed)
                {
                    _scrollbar.value = Mathf.Lerp(1, 0, timer / speed);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    if (elapsed >= totalTime)
                    {
                        yield break;
                    }
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
        float perfectAreaPos = _perfectAreaRect.anchoredPosition.x;

        float trueAreaStart = trueAreaPos - (_trueAreaRect.rect.width * _trueAreaRect.pivot.x);
        float trueAreaEnd = trueAreaStart + _trueAreaRect.rect.width;

        float perfectAreaStart = perfectAreaPos - (_perfectAreaRect.rect.width * _perfectAreaRect.pivot.x);
        float perfectAreaEnd = perfectAreaStart + _perfectAreaRect.rect.width;

        float trueStartValue = Mathf.Clamp01(trueAreaStart / slidingAreaWidth);
        float trueEndValue = Mathf.Clamp01(trueAreaEnd / slidingAreaWidth);

        float perfectStartValue = Mathf.Clamp01(perfectAreaStart / slidingAreaWidth);
        float perfectEndValue = Mathf.Clamp01(perfectAreaEnd / slidingAreaWidth);

        if (value >= trueStartValue && value <= trueEndValue)
        {
            JudgeTextUI judge = Managers.UI.MakeSubItem<JudgeTextUI>(transform, "JudgeText");
            if (value >= perfectStartValue && value <= perfectEndValue)
            {
                Debug.Log("MiniGame Perfect");
                judge.SetJudgeTextImage(perfectSprite, handle.transform.position);
                MoveRight();
                return true;
            }
            Debug.Log("MiniGame Success");
            judge.SetJudgeTextImage(greatSprite, handle.transform.position);
            MoveRight();
            return true;
        }
        else
        {
            Debug.Log("MiniGame Fail");
            JudgeTextUI judge = Managers.UI.MakeSubItem<JudgeTextUI>(transform, "JudgeText");
            judge.SetJudgeTextImage(missSprite, handle.transform.position);
            MoveLeft();
            return false;
        }
    }

    void MoveRight(float distance = 0f)
    {
        if (distance == 0f)
        {
            distance = moveDistance;
        }
        Vector2 newPos = characterRectTrnasform.anchoredPosition;
        newPos.x += distance;
        if (newPos.x >= rightProhibit.position.x)
        {
            newPos.x = rightProhibit.position.x;
        }
        characterRectTrnasform.anchoredPosition = newPos;

        CheckReachProhibitedArea();
    }

    void MoveLeft(float distance = 0f)
    {
        if (distance == 0f)
        {
            distance = moveDistance;
        }
        Vector2 newPos = characterRectTrnasform.anchoredPosition;
        newPos.x -= distance;
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
            MoveRight(characterRectTrnasform.rect.width);
            return true;
        }
        if (rightCheck)
        {
            StartCoroutine(BlinkCharacter());
            MoveLeft(characterRectTrnasform.rect.width);
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

            CheckReachProhibitedArea();
        }
    }

    IEnumerator ClockRotate()
    {
        float rotateSpeed = 360f / totalTime;
        float elapsed = 0f;

        while (elapsed < totalTime)
        {
            clockCenter.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    void RankDown()
    {
        int enableIdx;
        for(int i = 0; i < rankBar.childCount ; i++)
        {
            bool activeFlag = rankBar.GetChild(i).gameObject.activeSelf;
            if(activeFlag)
            {
                rankBar.GetChild(i).gameObject.SetActive(false);
                enableIdx = i;
                break;
            }
        }
        // TODO : Handle 조정 및 childBound 에러 수정
    }
}
