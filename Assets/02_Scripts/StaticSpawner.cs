using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectPosition = null;
    [SerializeField]
    GameObject objectPrefab = null;
    bool[] isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = new bool[objectPosition.Length];
        RandomSpawn();
    }

    // Spawns objects randomly at each position in the objectPosition array
    void RandomSpawn()
    {
        int spawnCount = Random.Range(1, objectPosition.Length);

        for(int i = 0; i < spawnCount; i++) 
        {
            int randomIndex = Random.Range(0, objectPosition.Length);

            if(!isActive[randomIndex])
            {
                isActive[randomIndex] = true;
                Instantiate(objectPrefab, objectPosition[randomIndex].transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            }
        }
    }
}
