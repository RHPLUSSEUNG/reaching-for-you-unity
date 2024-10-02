using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPassage : MonoBehaviour
{
    [SerializeField]
    private Stage[] stages;
    private GameObject[] wallPrefabs;

    int DoorCount = 0;

    List<Transform> passageChildren = new List<Transform>();
    public static List<BoxCollider> gimmickTrigger = new List<BoxCollider>();
    List<GameObject> wallChildren = new List<GameObject>();

    public void Init()
    {
        foreach (Transform t in transform)
        {
            BoxCollider collider = t.Find("Interaction").GetComponent<BoxCollider>();
            passageChildren.Add(t);
            gimmickTrigger.Add(collider);
        }

        DoorCount = Random.Range(0, passageChildren.Count - 1) + 1;
        
        InitWallPrefab();
        AddPassage();
    }

    private void InitWallPrefab()
    {
        int index = AdventureManager.StageNumber;
        wallPrefabs = new GameObject[2];
        
        for(int i = 0; i < stages[index].stageOfPrefabs.Length; i++)
        {
            wallPrefabs[i] = stages[index].stageOfPrefabs[i];
        }
    }

    public void AddPassage()
    {
        int index = 0;
        GameObject wallInMap;
        
        for(; index < DoorCount; index++)
        {
            wallInMap = Instantiate(wallPrefabs[1], passageChildren[index]);
            wallInMap.transform.SetParent(passageChildren[index]);

            if(AdventureManager.StageNumber > 0 && AdventureManager.isGimmickRoom)
            {
                gimmickTrigger[index].gameObject.tag = "Untagged";
                gimmickTrigger[index].isTrigger = false;
            }
                
            else
            {
                gimmickTrigger[index].gameObject.tag = "Teleport";
                gimmickTrigger[index].isTrigger = true;
            }
                
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
