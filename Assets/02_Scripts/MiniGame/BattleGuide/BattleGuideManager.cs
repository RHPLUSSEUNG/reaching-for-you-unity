
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleGuideManager : MonoBehaviour
{
    public float currentTime;
    public float score;
    private bool isPlaying;
    private float hitTimer;
    private float spawnTimer;
    private float randSpwanTime;
    private bool canHit;
    private bool canSpawnTime;

    protected int currentHitZone;
    protected int currnetStage;

    [Header("Game Setting")]
    [SerializeField]
    Text timeText;
    [SerializeField]
    Text stageText;
    [SerializeField]
    public float maxTime;
    [SerializeField]
    GameObject player;
    [SerializeField]
    protected int life;
    [SerializeField]
    protected float hitInterval;
    [SerializeField]
    GameObject plane;
    [SerializeField]
    GameObject hitZone;

    BoxCollider boxCollider;


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
    protected float minFillSpeed_plus;
    [SerializeField]
    [Range(0f, 5f)]
    protected float maxFillSpeed_plus;


    private void Awake()
    {
        currnetStage = 1;
        hitTimer = 0;
        currentTime = maxTime;
        currentHitZone = 0;
        isPlaying = false;
        canHit = true;
        boxCollider = plane.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        StartCoroutine(WaitAndExecute());
    }

    public void FixedUpdate()
    {
        if (isPlaying)
        {
            currentTime = currentTime - Time.deltaTime;
            timeText.text = currentTime.ToString("F2");
            if (currentTime <= 0)
            {
                isPlaying = false;
                NextStage();
            }
        }
    }
    public void spawnHitZone()
    {
        currentHitZone++;
        canSpawnTime = false;
        spawnTimer = 0;
        GameObject hitzone = Instantiate(hitZone, GetRandPosition(), Quaternion.Euler(90, 0, 0));
        float radius = GetRandRadiusSize();
        hitzone.transform.localScale = new Vector3(radius, radius,1);
        hitzone.GetComponentInChildren<CircleMaskFill>().fillSpeed= GetRandFillSpeed();
        randSpwanTime = GetRandSpawnTime();
    }
    public Vector3 GetRandPosition()
    { 
        Vector3 center = plane.transform.position;
        float width = boxCollider.bounds.size.x;
        float height = boxCollider.bounds.size.z;

        float randX = Random.Range(-width / 2f, width / 2f);
        float randZ = Random.Range(-height / 2f, height / 2f);

        float y = center.y+0.1f;

        Vector3 randPoint = new Vector3(center.x + randX, y, center.z + randZ);

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
    public void Hit()
    {
        if (!canHit)
            return;

        life--;
        Debug.Log("Hit");
        HitTimeCheck();
        if (life <= 0 )
        {
            isPlaying = false;
            GameOver();
        }
    }

    public void GameOver()
    {
        StopAllCoroutines();
        score = currentTime * life;
    }
    public void NextStage()
    {

        currnetStage += 1;
        maxSpawnCount += maxSpawnCount_plus;
        if (minSpwanInterval - minSpwanInterval_minus <= 0) //예외처리
            minSpwanInterval = 0;
        else
            minSpwanInterval -= minSpwanInterval_minus;

        if (maxSpwanInterval - maxSpwanInterval_minus <= 0) //예외처리
            minSpwanInterval = 0;
        else
            maxSpwanInterval -= maxSpwanInterval_minus;
        minRadius = minRadius_plus;
        maxRadius =maxRadius_plus;
        minFillSpeed = minFillSpeed_plus;
        maxFillSpeed = maxFillSpeed_plus;

        currentTime = maxTime;

        StartCoroutine(WaitAndExecute());
    }
    void HitTimeCheck()
    {
        canHit = false;
        hitTimer = 0;
        StartCoroutine(HitTimer());
    }

    IEnumerator HitTimer()
    {
        //플레이어 무적 이펙트
        while (hitTimer < hitInterval)
        {
            hitTimer += Time.deltaTime;
            yield return null;
        }
        canHit = true;
    }
    IEnumerator spawnHitZoneCoroutine()
    {
        while (!canSpawn())
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= randSpwanTime)
                canSpawnTime = true;

            yield return null;
        }

        spawnHitZone();
        StartCoroutine(spawnHitZoneCoroutine());
    }
    IEnumerator WaitAndExecute()
    {
        stageText.text = currnetStage.ToString();
        yield return new WaitForSeconds(3f); // 3초 대기
        isPlaying = true;
        StartCoroutine(spawnHitZoneCoroutine());
    }
    bool canSpawn()
    {
        if (currentHitZone >= maxSpawnCount || !canSpawnTime)
            return false;
        else
            return true;
    }
    public void HitZoneDestroy()
    {
        currentHitZone--;
    }
}
