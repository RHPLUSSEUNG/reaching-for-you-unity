using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject planeObject;
    [SerializeField]
    GameObject objectPrefab = null;
    BoxCollider rangeCollider;

    public int maxSpawnCount = 10;
    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Awake()
    {
        rangeCollider = planeObject.GetComponent<BoxCollider>();
    }

    void Start()
    {
        RandomSpawn();
    }

    Vector3 RandomPosition()
    {
        // (0,0,0)
        Vector3 planePosition = planeObject.transform.position;

        // Plane Size
        float rangeX = rangeCollider.bounds.size.x;
        float rangeZ = rangeCollider.bounds.size.z;

        // RandomPosition Range
        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 randomPosition = new Vector3(rangeX, 0.5f, rangeZ);

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

    void RandomSpawn() {
        int spawnCount = Random.Range(1, maxSpawnCount);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = RandomPosition();
            } while (IsOverlapping(spawnPosition)); // If overlapping, re-position
            spawnedPositions.Add(spawnPosition);

            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
