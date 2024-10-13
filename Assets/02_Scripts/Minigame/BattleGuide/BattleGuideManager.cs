
using System.Collections;
using UnityEngine;

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

    [Header("Game Setting")]
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


    [Header("Phase 1")]
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

    private void Awake()
    {
        hitTimer = 0;
        currentTime = 0;
        life = 3;
        maxTime = 100;
        currentHitZone = 0;
        isPlaying = true;
        canHit = true;
        boxCollider = plane.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        spawnHitZone();
        StartCoroutine(spawnHitZoneCoroutine());
    }

    public void FixedUpdate()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                isPlaying = false;
                GameOver();
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
