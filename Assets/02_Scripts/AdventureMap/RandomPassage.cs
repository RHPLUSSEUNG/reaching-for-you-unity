using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public string name;
    public GameObject[] wallPrepavs;
}

public class RandomPassage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wallPrefabs;
    [SerializeField]
    private Stage[] stages;

    int DoorCount = 0;
    int stageIndex;

    List<Transform> passageChildren = new List<Transform>();
    List<GameObject> wallChildren = new List<GameObject>();

    public void Init()
    {
        foreach (Transform t in transform)
        {
            passageChildren.Add(t);
        }

        DoorCount = Random.Range(0, passageChildren.Count - 1) + 1;
        stageIndex = AdventureManager.StageNumber;
        AddPassage();
    }

    public void AddPassage()
    {
        int index = 0;
        GameObject wallInMap;
        
        for(; index < DoorCount; index++)
        {
            wallInMap = Instantiate(wallPrefabs[(2 * stageIndex) + 1], passageChildren[index]);
            wallInMap.transform.SetParent(passageChildren[index]);

            wallChildren.Add(wallInMap);
        }

        for(; index < passageChildren.Count; index++)
        {
            wallInMap = Instantiate(wallPrefabs[2 * stageIndex], passageChildren[index]);
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
