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

    public int maxSpawnCount = 10;
    public static List<Vector3> spawnedPositions = new List<Vector3>();
    private List<GameObject> spawnedObject = new List<GameObject>();

    void Awake()
    {
        rangeCollider = planeObject.GetComponent<BoxCollider>();
    }

    void Start()
    {
        // RandomSpawn();
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

    public void DestroyObject()
    {
        foreach(GameObject o in spawnedObject)
        {
            Destroy(o);
        }
    }
}
