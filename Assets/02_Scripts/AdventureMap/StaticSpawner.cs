using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectPosition = null;
    [SerializeField]
    GameObject[] objectPrefab = null;
    bool[] isActive;

    private List<GameObject> spawnedObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        isActive = new bool[objectPosition.Length];
        // RandomSpawn();
    }

    // Spawns objects randomly at each position in the objectPosition array
    public void RandomSpawn()
    {
        DestroyObject();
        int spawnCount = Random.Range(1, objectPosition.Length);

        for(int i = 0; i < spawnCount; i++) 
        {
            int randomPositionIndex = Random.Range(0, objectPosition.Length);
            int randomIndex = Random.Range(0, objectPrefab.Length);

            Vector3 spawnPosition;

            if(!isActive[randomPositionIndex])
            {
                isActive[randomPositionIndex] = true;
                spawnPosition = objectPosition[randomPositionIndex].transform.position + objectPrefab[randomIndex].transform.position;

                GameObject spawned = Instantiate(objectPrefab[randomIndex], spawnPosition, Quaternion.identity);
                spawnedObject.Add(spawned);
            }
        }
    }

    public void DestroyObject()
    {
        foreach(GameObject o in spawnedObject)
        {
            Destroy(o);
        }

        for(int i = 0; i < isActive.Length; i++)
        {
            isActive[i] = false;
        }
    }
}
