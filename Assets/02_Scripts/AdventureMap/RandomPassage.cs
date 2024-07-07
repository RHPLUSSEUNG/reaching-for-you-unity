using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPassage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wallPrefabs;

    int DoorCount = 0;

    List<Transform> passageChildren = new List<Transform>();
    List<GameObject> wallChildren = new List<GameObject>();

    void Start()
    {
        foreach (Transform t in transform)
        {
            passageChildren.Add(t);
        }

        DoorCount = Random.Range(0, passageChildren.Count - 1) + 1;
        AddPassage();
    }

    public void AddPassage()
    {
        int index = 0;
        GameObject wallInMap;
        
        for(; index < DoorCount; index++)
        {
            wallInMap = Instantiate(wallPrefabs[1], passageChildren[index]);
            wallInMap.transform.SetParent(passageChildren[index]);

            wallChildren.Add(wallInMap);
        }

        for(; index < passageChildren.Count; index++)
        {
            wallInMap = Instantiate(wallPrefabs[0], passageChildren[index]);
            wallInMap.transform.SetParent(passageChildren[index]);

            wallChildren.Add(wallInMap);
        }
    }

    public void DeletePassage() 
    {
        DoorCount = Random.Range(0, passageChildren.Count) + 1;
        foreach(GameObject wall in wallChildren)
        {
            Destroy(wall);
        }
    }
}
