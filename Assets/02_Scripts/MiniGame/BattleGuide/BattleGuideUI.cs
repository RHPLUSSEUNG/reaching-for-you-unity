using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BattleGuideUI : MiniGameBase
{
    enum battleGuideUI
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
    [SerializeField]
    GameObject handle;
    [SerializeField]
    List<GameObject> HPsprite;
    [SerializeField]
    List<Sprite> rank = new List<Sprite>();
    [SerializeField]
    float minBoundsX;
    [SerializeField]
    float maxBoundsX;
    [SerializeField]
    float minBoundsY;
    [SerializeField]
    float maxBoundsY;

    RectTransform characterRectTrnasform;
    RectTransform leftProhibit;
    RectTransform rightProhibit;
    RectTransform clockCenter;

    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI countdownText;


   //int stageNumber = 1;

    bool isInvincible = false;
    bool isProgress = false;

    Vector2 inputVec;
    Vector2 nextVec;
    Vector3 initialPos;

    [Header("Game Setting")]
    [SerializeField]
    Text timeText;
    [SerializeField]
    Text stageText;
    [SerializeField]
    public float totalTime;
    [SerializeField]
    protected int life;
    [SerializeField]
    BoxCollider2D spawnZone;
    int currentHitZone = 0;
    bool canSpawnTime = true;
    float spawnTimer = 0;
    float currnetTime = 0;
    float nextRandSpwanTime;

    [Header("Player Setting")]
    [SerializeField]
    GameObject player;
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rollSpeed = 5f;
    [SerializeField]
    float rollDuration = 0.5f;
    [SerializeField]
    protected float hitInterval;
    RectTransform playerCollider;

    Animator playerAnim;
    float hitTimer = 0;
    bool isRolling = false;
    bool isHit = false;

    [Header("Object Pool Setting")]
    [SerializeField]
    GameObject hitZone;
    [SerializeField]
    int poolSize = 10;

    Queue<GameObject> hitZonePool;

    #region Difficulty Setting
    [Header("Basic Value")]
    [SerializeField]
    protected int maxSpawnCount;
    [SerializeField]
    [Range(0f, 3f)]
    protected float minSpwanInterval;
    [SerializeField]
    [Range(0f, 3f)]
    protected float maxSpwanInterval;
    [SerializeField]
    [Range(0f, 5f)]
    protected float minRadius;
    [SerializeField]
    [Range(0f, 5f)]
    protected float maxRadius;
    [Range(0f, 5f)]
    [SerializeField]
    protected float minFillSpeed;
    [SerializeField]
    [Range(0f, 5f)]
    protected float maxFillSpeed;

    [Header("Stage Value")]
    [SerializeField]
    protected int maxSpawnCount_plus;
    [SerializeField]
    [Range(0f, 3f)]
    protected float minSpwanInterval_minus;
    [SerializeField]
    [Range(0f, 3f)]
    protected float maxSpwanInterval_minus;
    [SerializeField]
    [Range(0f, 5f)]
    protected float minRadius_plus;
    [SerializeField]
    [Range(0f, 5f)]
    protected float maxRadius_plus;
    [Range(0f, 5f)]
    [SerializeField]
    protected float minFillSpeed_minus;
    [SerializeField]
    [Range(0f, 5f)]
    protected float maxFillSpeed_minus;
    #endregion


    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(battleGuideUI));

        timeLimit = GetObject((int)battleGuideUI.TimeLimit).GetComponent<Slider>();
        stageNumberText = GetObject((int)battleGuideUI.StageNumberText).GetComponent<TextMeshProUGUI>();
        countdownText = GetObject((int)battleGuideUI.CountDownText).GetComponent<TextMeshProUGUI>();
        clockCenter = GetObject((int)battleGuideUI.ClockCenter).GetComponent<RectTransform>();
        //rankImg = GetObject((int)battleGuideUI.RankImage).GetComponent<Image>();
        //characterRectTrnasform = GetObject((int)battleGuideUI.Character).GetComponent<RectTransform>();
        playerCollider = player.transform.GetChild(0).GetComponent<RectTransform>();
        playerAnim = GetObject((int)battleGuideUI.Character).GetComponent<Animator>();
        rigid = player.GetComponent<Rigidbody2D>();

        //initialPos = characterRectTrnasform.anchoredPosition;
        countdownText.gameObject.SetActive(false);

        GenerateObjectPool();
        SoundManager.Instance.StopMusic();
        StartCoroutine(Countdown());
    }

    private void FixedUpdate()
    {
        if(isProgress && !isHit)
        {
            CharacterControl();
        }
    }

    public int GetStageLevel()
    {
        return stageNumber;
    }

    public void SetLevel()
    {
        life = 3;
        maxSpawnCount += maxSpawnCount_plus;
        minRadius += minRadius_plus;
        maxRadius += maxRadius_plus;
        if ((minFillSpeed - minFillSpeed_minus) != 0)
            minFillSpeed -= minFillSpeed_minus;
        else
            minFillSpeed = 0.2f;
        if ((maxFillSpeed - maxFillSpeed_minus) != 0)
            maxFillSpeed -= maxFillSpeed_minus;
        else
            maxFillSpeed = 0.2f;
        if ((minSpwanInterval - minSpwanInterval_minus)!=0)
            minSpwanInterval -= minSpwanInterval_minus;
        if ((maxSpwanInterval - maxSpwanInterval) != 0)
            maxSpwanInterval -= maxSpwanInterval_minus;
}
    void CharacterControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //±¸¸£±â
        {
            StartRoll();
        }

        if (isRolling)
        {
            nextVec = (inputVec.normalized) * rollSpeed;
        }
        else
        {
            Move();
        }

        Vector2 currentPosition = rigid.position;

        Vector2 clampedPosition = new Vector2(Mathf.Clamp(currentPosition.x, minBoundsX, maxBoundsX), Mathf.Clamp(currentPosition.y, minBoundsY, maxBoundsY));

        if (clampedPosition != currentPosition)
            rigid.position = clampedPosition;

        if(currentPosition.x <= minBoundsX && nextVec.x < 0) nextVec.x = 0;
        if (currentPosition.x >= maxBoundsX && nextVec.x > 0) nextVec.x = 0;

        if (currentPosition.y <= minBoundsY && nextVec.y < 0) nextVec.y = 0;
        if (currentPosition.y >= maxBoundsY && nextVec.y > 0) nextVec.y = 0;

        rigid.velocity = nextVec;
    }
    void Move()
    {
        inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputVec.x > 0)
        {
            player.transform.eulerAngles = new Vector2(0, 180);
        }
        else if (inputVec.x < 0)
        {
            player.transform.eulerAngles = new Vector2(0, 0);
        }
        if (inputVec.x == 0 && inputVec.y == 0)
        {
            playerAnim.SetInteger("State", 0);
        }
        else
        {
            playerAnim.SetInteger("State", 1);
        }
        nextVec = (inputVec.normalized) * moveSpeed;
    }
    void StartRoll()
    {
        isRolling = true;
        playerAnim.SetInteger("State", 3);
        isInvincible = true;
    }
    public void EndRoll()
    {
        isInvincible = false;
        isRolling = false;
        rigid.velocity = Vector2.zero;
        playerAnim.SetInteger("State", 0);
    }
    public void Hit()
    {
        if (isInvincible)
            return;

        //playerAnim.SetInteger("State", 2);
        playerAnim.SetTrigger("Hit");
        rigid.velocity = Vector2.zero;
        SoundManager.Instance.PlaySFX("SFX_PracticalCombat_Fail_01"); 
        rigid.velocity = Vector2.zero;
        isHit = true;
        isInvincible = true;
        StartCoroutine(BlinkCharacter());


        life--;
        HPsprite[life].SetActive(false);

        if (life <= 0)
        {
            life = 3;
            GameOver();
            return;
        }

    }
    public void EndHit()
    {
        isHit = false;
        playerAnim.SetInteger("State", 0);
    }
    IEnumerator MiniGameStart()
    {
        foreach (GameObject obj in HPsprite)
        {
            obj.SetActive(true);
        }
        isProgress = true;
        if(stageNumber == 1)
        {
            SoundManager.Instance.PlayMusic("BGM_MiniGame_PracticalCombat_01");
        }
        else
        {
            SetLevel();
        }
        Debug.Log("Game Start");
        float elapsed = 0f;

        playerAnim.SetInteger("State", 0);
        StartCoroutine(TimeLimitStart());
        StartCoroutine(ClockRotate());
        StartCoroutine(spawnHitZoneCoroutine());
        while (elapsed < totalTime)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= totalTime)
            {
                Debug.Log("Game End");
                GameClear();
                yield break;
            }

            yield return null;
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

        Image character = GetObject((int)battleGuideUI.Character).GetComponent<Image>();
        Color characterColor = character.color;

        while (timer < hitInterval)
        {
            character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 0f);
            yield return new WaitForSeconds(0.05f);
            character.color = characterColor;
            yield return new WaitForSeconds(0.05f);

            timer += 0.1f;
        }
        character.color = characterColor;
        playerAnim.SetInteger("State", 0);
        isInvincible = false;
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

    IEnumerator Countdown()
    {
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
        rigid.velocity= Vector2.zero;
        Image character = GetObject((int)battleGuideUI.Character).GetComponent<Image>();
        Color characterColor = character.color;
        character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 1f);
        playerAnim.SetInteger("State", 0);
        SoundManager.Instance.PlaySFX("SFX_BasicPhysiology_Success_01");

        GameClearPopupUI clearUI = Managers.UI.CreatePopupUI<GameClearPopupUI>("GameClearPopup");
        clearUI.gameUI = gameObject.GetComponent<BattleGuideUI>();
        clearUI.SetRankImage(rank[life]);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        isProgress = false;
        rigid.velocity = Vector2.zero;
        StopAllCoroutines();
        Image character = GetObject((int)battleGuideUI.Character).GetComponent<Image>();
        Color characterColor = character.color;
        character.color = new Color(characterColor.r, characterColor.g, characterColor.b, 1f);
        playerAnim.SetInteger("State", 0);
        SoundManager.Instance.PlaySFX("SFX_MagicTheory_Miss_01");

        GameOverPopupUI overUI = Managers.UI.CreatePopupUI<GameOverPopupUI>("GameOverPopup");
        overUI.gameUI = gameObject.GetComponent<BattleGuideUI>();
        overUI.SetRankImage();
    }

    public void SpawnHitZone()
    {
        currentHitZone++;
        canSpawnTime = false;
        spawnTimer = 0;
        GameObject hitzone = GetFromPool();
        hitzone.transform.position = GetRandPosition();
        float radius = GetRandRadiusSize();
        SoundManager.Instance.PlaySFX("SFX_PracticalCombat_Attack_01");
        hitzone.transform.localScale = new Vector3(radius, radius, 1);
        hitzone.GetComponentInChildren<CircleFill>().fillSpeed = GetRandFillSpeed();
        nextRandSpwanTime = GetRandSpawnTime();
    }
    IEnumerator spawnHitZoneCoroutine()
    {
        while (isProgress)
        {
            spawnTimer += Time.deltaTime;
            if (currentHitZone < maxSpawnCount && spawnTimer >= nextRandSpwanTime)
            {
                SpawnHitZone();
                Debug.Log("spawn");
            }

            yield return new WaitForFixedUpdate();
        }
    }
    public Vector2 GetRandPosition()
    {
        Vector2 center = spawnZone.transform.position;
        float width = spawnZone.bounds.size.x;
        float height = spawnZone.bounds.size.z;

        float randX = Random.Range(-width / 2f, width / 2f);
        float randY = Random.Range(-height / 2f, height / 2f);

        float y = center.y + 0.1f;

        Vector2 randPoint = new Vector2(center.x + randX, center.y + randY);

        return randPoint;
    }
    float GetRandSpawnTime()
    {
        return Random.Range(minSpwanInterval, maxSpwanInterval);
    }
    float GetRandRadiusSize()
    {
        return Random.Range(minRadius, maxRadius);
    }
    float GetRandFillSpeed()
    {
        return Random.Range(minFillSpeed, maxFillSpeed);
    }
    public void GenerateObjectPool()
    {
        hitZonePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(hitZone,this.transform);
            obj.SetActive(false);
            hitZonePool.Enqueue(obj);
        }
    }
    public GameObject GetFromPool()
    {
        if (hitZonePool.Count > 0)
        {
            GameObject obj = hitZonePool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(hitZone,this.transform);
            return obj;
        }
    }
    public void ReturnToPool(GameObject obj)
    {
        currentHitZone--;
        obj.SetActive(false);
        hitZonePool.Enqueue(obj);
    }
    public void CheckOverlap (RectTransform hitzone)
    {
        Rect characterRect = GetWorldRect(playerCollider);
        Rect prohibitRect = GetWorldRect(hitzone);

        if(characterRect.Overlaps(prohibitRect))
        {
            Hit();
        }
    }

    public override void GameEnd()
    {
        Debug.Log("Game End");
        SoundManager.Instance.PlayMusic("BGM_Academy_01");
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ChangeActive(true);
        Managers.Prefab.Destroy(gameObject);
    }

    public override void NextLevel()
    {
        stageNumber++;
        stageNumberText.text = $"Stage : {stageNumber}";
        StartCoroutine(Countdown());
    }
}
