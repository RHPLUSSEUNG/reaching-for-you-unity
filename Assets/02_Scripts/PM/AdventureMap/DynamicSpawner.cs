using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DynamicSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject planeObject;
    [SerializeField]
    private Stage[] stages;
    BoxCollider rangeCollider;

    [SerializeField]
    private GameObject waterRemoveGimmick;
    [SerializeField]
    private GameObject waterGimmick;
    private float gimmickCount;

    public static GameObject go_water;
    private Vector3 originPos;
    private Vector3 newVec;

    public int maxSpawnCount = 10;
    public static List<Vector3> spawnedPositions = new List<Vector3>();
    private List<GameObject> spawnedObject = new List<GameObject>();

    void Awake()
    {
        rangeCollider = planeObject.GetComponent<BoxCollider>();
        originPos = waterGimmick.transform.position;

        newVec = new Vector3(originPos.x, originPos.y * -1, originPos.z);

        go_water = Instantiate(waterGimmick, newVec, Quaternion.identity);
        spawnedPositions.Add(new Vector3(0f, 0.66f, 0f)); // 플레이어 시작 위치에서 스폰 X
    }

    void Start()
    {
        // RandomSpawn();
        gimmickCount = waterRemoveGimmick.GetComponentInChildren<WaterGimmickInAdventure>().gimmickCount;
    }

    void Update()
    {
        if(AdventureManager.isGimmickRoom)
        {
            gimmickCount -= Time.deltaTime;
            if(gimmickCount < 0f)
                SceneChanger.Instance.ChangeScene(SceneType.ACADEMY);
        }
    }

    Vector3 RandomPosition()
    {
        // (0,0,0)
        Vector3 planePosition = planeObject.transform.position;

        // Plane Size
        float rangeX = rangeCollider.bounds.size.x - 2.0f;
        float rangeZ = rangeCollider.bounds.size.z - 2.0f;

        // RandomPosition Range
        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 randomPosition = new Vector3(rangeX, 0.0f, rangeZ);

        Vector3 spawnPosition = planePosition + randomPosition;

        return spawnPosition;
    }

    bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < 1.0f)
            {
                return true;
            }
        }
        return false;
    }

    public void RandomSpawn() {
        DestroyObject();

        int index = AdventureManager.StageNumber;

        int spawnCount = Random.Range(1, maxSpawnCount);
        for (int i = 0; i < spawnCount; i++)
        {
            int objectNum = Random.Range(0, stages[index].stageOfPrefabs.Length);
            Vector3 spawnPosition;
            do
            {
                spawnPosition = RandomPosition();
            } while (IsOverlapping(spawnPosition)); // If overlapping, re-position
            spawnPosition += stages[index].stageOfPrefabs[objectNum].transform.position;
            spawnedPositions.Add(spawnPosition);

            GameObject spawned = Instantiate(stages[index].stageOfPrefabs[objectNum], spawnPosition, Quaternion.identity);
            spawnedObject.Add(spawned);
        }
    }

    public void RandomGimmick()
    {
        int spawnCount = AdventureManager.GimmickCount;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = RandomPosition();
            } while (IsOverlapping(spawnPosition)); // If overlapping, re-position
            spawnPosition += waterRemoveGimmick.transform.position;
            spawnedPositions.Add(spawnPosition);

            GameObject spawned = Instantiate(waterRemoveGimmick, spawnPosition, Quaternion.identity);
            spawnedObject.Add(spawned);
        }
    }

    public void DestroyObject()
    {
        foreach(GameObject o in spawnedObject)
        {
            Destroy(o);
        }
    }

    public void FillWater()
    {
        go_water.transform.position = originPos;
    }

    public void ClearWater()
    {
        go_water.transform.position = newVec;
    }
}
