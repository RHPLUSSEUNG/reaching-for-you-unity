using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicHealthUI : MiniGameBase
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
        Handle,
        RankImage,
        RankBar,
        StageNumberText,
        CountDownText
    }

    [SerializeField]
    Slider timeLimit;
    [SerializeField]
    Scrollbar _timingbar;
    RectTransform _failAreaRect;
    [SerializeField]
    RectTransform _trueAreaRect;
    [SerializeField]
    GameObject handle;
    [SerializeField]
    RectTransform rankBar;
    [SerializeField]
    Sprite greatSprite;
    [SerializeField]
    Sprite missSprite;
    [SerializeField]
    List<Sprite> rank = new List<Sprite>();

    RectTransform characterRectTrnasform;
    RectTransform leftProhibit;
    RectTransform rightProhibit;
    RectTransform clockCenter;

    Image rankImg;

    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI countdownText;
    Animator playerAnim;

    int life = 0;
    int lifeEnd = 4;
    float knockbackTimeOffset = 1.0f;
    float speedOffset = 0.3f;
    float knockbackDistanceOffset = 50f;

    float totalTime = 20f;
    float baseSpeed = 2.5f;
    float baseknockbackTime = 2.0f;
    float baseknockbackDistance = 150f;
    float moveDistance = 150f;
    float invincibilityTime = 1.0f;

    float speed;
    float knockbackTime;
    float knockbackDistance;
    float knockBackElapsed = 0f;

    Vector3 initialPos;
    float trueAreaMin = 200f;
    float trueAreaMax = 300f;

    bool isIncreasing = true;
    bool isInvincible = false;
    bool isProgress = false;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(basicHealthUI));

        timeLimit = GetObject((int)basicHealthUI.TimeLimit).GetComponent<Slider>();
        _timingbar = GetObject((int)basicHealthUI.TimingBar).GetComponent<Scrollbar>();
        _failAreaRect = GetObject((int)basicHealthUI.FailArea).GetComponent<RectTransform>();
        _trueAreaRect = GetObject((int)basicHealthUI.TrueArea).GetComponent<RectTransform>();
        rankBar = GetObject((int)basicHealthUI.RankBar).GetComponent<RectTransform>();
        handle = GetObject((int)basicHealthUI.Handle);
        rankImg = GetObject((int)basicHealthUI.RankImage).GetComponent<Image>();
        stageNumberText = GetObject((int)basicHealthUI.StageNumberText).GetComponent<TextMeshProUGUI>();
        countdownText = GetObject((int)basicHealthUI.CountDownText).GetComponent<TextMeshProUGUI>();
        clockCenter = GetObject((int)basicHealthUI.ClockCenter).GetComponent<RectTransform>();

        characterRectTrnasform = GetObject((int)basicHealthUI.Character).GetComponent<RectTransform>();
        leftProhibit = GetObject((int)basicHealthUI.LeftProhibitedArea).GetComponent<RectTransform>();
        rightProhibit = GetObject((int)basicHealthUI.RightProhibitedArea).GetComponent<RectTransform>();
        playerAnim = GetObject((int)basicHealthUI.Character).GetComponent<Animator>();

        initialPos = characterRectTrnasform.anchoredPosition;
        countdownText.gameObject.SetActive(false);

        SoundManager.Instance.StopMusic();
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        if(isProgress)
        {
            if (!isInvincible)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    bool check = CheckTrueArea(_timingbar.value);
                    if (check)
                    {
                        SetTrueArea();
                    }
                }
            }
        }
    }

    public void SetLevel()
    {
        speed = baseSpeed - (speedOffset * stageNumber);
        knockbackTime = baseknockbackTime;
        // knockbackTime = baseknockbackTime - (knockbackTimeOffset * stageNumber);
        knockbackDistance = baseknockbackDistance;
        // knockbackDistance = baseknockbackDistance + (knockbackDistanceOffset * stageNumber);
    }

    void SetTrueArea()
    {
        float rangeValue = Random.Range(5f, 95f);
        float trueSize = Random.Range(trueAreaMin, trueAreaMax);

        _trueAreaRect.gameObject.SetActive(true);
        _trueAreaRect.anchoredPosition = new Vector2(Mathf.Lerp(0, _failAreaRect.sizeDelta.x, rangeValue / 100f), 0);
        _trueAreaRect.sizeDelta = new Vector2(trueSize, _failAreaRect.sizeDelta.y);
    }

    IEnumerator MiniGameStart()
    {
        isProgress = true;
        if(stageNumber == 1)
        {
            SoundManager.Instance.PlayMusic("BGM_MiniGame_BasicPhysiology_01");
        }
        Debug.Log("Game Start");
        SetLevel();
        SetTrueArea();
        float elapsed = 0f;

        playerAnim.SetBool("isProgress", true);
        StartCoroutine(TimeLimitStart());
        StartCoroutine(ClockRotate());
        while (elapsed < totalTime)
        {
            float timer = 0f;

            if (isIncreasing)
            {
                while (timer < speed)
                {
                    _timingbar.value = Mathf.Lerp(0, 1, timer / speed);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    knockBackElapsed += Time.deltaTime;

                    if (knockBackElapsed >= knockbackTime && !isInvincible)
                    {
                        KnockBackCharacter();
                        knockBackElapsed = 0f;
                    }
                    if (elapsed >= totalTime)
                    {
                        Debug.Log("Game End");
                        GameClear();
                        yield break;
                    }
                    yield return null;
                }
            }
            else
            {
                while (timer < speed)
                {
                    _timingbar.value = Mathf.Lerp(1, 0, timer / speed);
                    timer += Time.deltaTime;
                    elapsed += Time.deltaTime;
                    knockBackElapsed += Time.deltaTime;

                    if (knockBackElapsed >= knockbackTime && !isInvincible)
                    {
                        KnockBackCharacter();
                        knockBackElapsed = 0f;
                    }
                    if (elapsed >= totalTime)
                    {
                        Debug.Log("Game End");
                        GameClear();
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
        RectTransform slidingArea = _timingbar.GetComponent<RectTransform>();
        float slidingAreaWidth = slidingArea.rect.width;

        float trueAreaPos = _trueAreaRect.anchoredPosition.x;

        float trueAreaStart = trueAreaPos - (_trueAreaRect.rect.width * _trueAreaRect.pivot.x);
        float trueAreaEnd = trueAreaStart + _trueAreaRect.rect.width;

        float trueStartValue = Mathf.Clamp01(trueAreaStart / slidingAreaWidth);
        float trueEndValue = Mathf.Clamp01(trueAreaEnd / slidingAreaWidth);

        if (value >= trueStartValue && value <= trueEndValue)
        {
            JudgeTextUI judge = Managers.UI.MakeSubItem<JudgeTextUI>(transform, "JudgeText");
            Debug.Log("MiniGame Success");
            judge.SetJudgeTextImage(greatSprite, handle.transform.position);
            MoveLeft();
            return true;
        }
        else
        {
            Debug.Log("MiniGame Fail");
            JudgeTextUI judge = Managers.UI.MakeSubItem<JudgeTextUI>(transform, "JudgeText");
            judge.SetJudgeTextImage(missSprite, handle.transform.position);
            MoveRight();
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

        if(leftCheck || rightCheck)
        {
            isInvincible = true;
            life++;
            knockBackElapsed = 0f;
            playerAnim.SetBool("isHit", true);
            StartCoroutine(BlinkCharacter());
            Vector2 newPos = characterRectTrnasform.anchoredPosition;
            SoundManager.Instance.PlaySFX("SFX_BasicPhysiology_Hit_01");
            if (leftCheck)
            {
                newPos.x = leftProhibit.position.x + characterRectTrnasform.rect.width;
                characterRectTrnasform.anchoredPosition = newPos;
            }
            else if(rightCheck)
            {
                newPos.x = rightProhibit.position.x - characterRectTrnasform.rect.width;
                characterRectTrnasform.anchoredPosition = newPos;
            }

            RankDown();
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
        float timer = 0f;

        Image character = GetObject((int)basicHealthUI.Character).GetComponent<Image>();
        Color characterColor = character.color;

        while (timer < invincibilityTime)
        {
            character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 0f);
            yield return new WaitForSeconds(0.05f);
            character.color = characterColor;
            yield return new WaitForSeconds(0.05f);

            timer += 0.1f;
        }
        character.color = characterColor;
        playerAnim.SetBool("isHit", false);
        isInvincible = false;
    }

    void KnockBackCharacter()
    {
        Vector2 newPos = characterRectTrnasform.anchoredPosition;
        newPos.x += knockbackDistance;
        if (newPos.x <= leftProhibit.position.x)
        {
            newPos.x = leftProhibit.position.x;
        }
        characterRectTrnasform.anchoredPosition = newPos;

        CheckReachProhibitedArea();
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
        if(life >= lifeEnd)
        {
            life = 4;
            GameOver();
            return;
        }

        rankImg.sprite = rank[life];
        int preRank = life - 1;
        rankBar.GetChild(preRank).gameObject.SetActive(false);
        rankBar.GetChild(life).gameObject.SetActive(true);
    }

    void GameSettingReset()
    {
        characterRectTrnasform.anchoredPosition = initialPos;
        _timingbar.value = 0f;
        rankBar.GetChild(life).gameObject.SetActive(false);
        life = 0;
        rankBar.GetChild(life).gameObject.SetActive(true);
        rankImg.sprite = rank[life];
        knockBackElapsed = 0f;
    }

    IEnumerator Countdown()
    {
        _trueAreaRect.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);

        Vector3 originalScale = countdownText.transform.localScale;
        Color countdownColor = countdownText.color;

        SoundManager.Instance.PlaySFX("SFX_StartSign_01");
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.transform.localScale = originalScale * 0.5f;

            for (float t = 0; t <0.5f; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / 0.5f);
                float scale = Mathf.Lerp(0.5f, 1f, t / 0.5f);
                countdownColor.a = alpha;
                countdownText.color = countdownColor;
                countdownText.transform.localScale = originalScale * scale;
                yield return null;
            }

            yield return new WaitForSeconds(0.25f);
        }

        countdownText.text = "Start!";;
        countdownText.transform.localScale = originalScale * 0.5f;

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / 0.5f);
            float scale = Mathf.Lerp(0.5f, 1f, t / 0.5f);
            countdownColor.a = alpha;
            countdownText.color = countdownColor;
            countdownText.transform.localScale = originalScale * scale;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false);
        StartCoroutine(MiniGameStart());
    }

    void GameClear()
    {
        isProgress = false;
        Image character = GetObject((int)basicHealthUI.Character).GetComponent<Image>();
        Color characterColor = character.color;
        character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 1f);
        playerAnim.SetBool("isProgress", false);
        SoundManager.Instance.PlaySFX("SFX_BasicPhysiology_Success_01");

        GameClearPopupUI clearUI = Managers.UI.CreatePopupUI<GameClearPopupUI>("GameClearPopup");
        clearUI.gameUI = gameObject.GetComponent<BasicHealthUI>();
        clearUI.SetRankImage(rankImg.sprite);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        isProgress = false;

        StopAllCoroutines();
        Image character = GetObject((int)basicHealthUI.Character).GetComponent<Image>();
        Color characterColor = character.color;
        character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 1f);
        playerAnim.SetBool("isProgress", false);
        SoundManager.Instance.PlaySFX("SFX_MagicTheory_Miss_01");

        GameOverPopupUI overUI = Managers.UI.CreatePopupUI<GameOverPopupUI>("GameOverPopup");
        overUI.gameUI = gameObject.GetComponent<BasicHealthUI>();
        overUI.SetRankImage();
    }
    public override void NextLevel()
    {
        GameSettingReset();
        stageNumber++;
        stageNumberText.text = $"Stage : {stageNumber}";
        StartCoroutine(Countdown());
    }
    public override void GameEnd()
    {
        Debug.Log("Game End");
        SoundManager.Instance.PlayMusic("BGM_Academy_01");
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(true);
        Managers.Prefab.Destroy(gameObject);
    }
}
