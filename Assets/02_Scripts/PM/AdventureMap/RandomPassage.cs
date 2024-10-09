using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class RandomPassage : MonoBehaviour
{
    [SerializeField]
    private Stage[] stages;
    private GameObject[] wallPrefabs;

    int DoorCount = 0;

    List<Transform> passageChildren = new List<Transform>();
    [SerializeField] List<BoxCollider> gimmickTrigger = new List<BoxCollider>();
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
                StartCoroutine(CloseDoor());

                WaterGimmickInAdventure[] gimmicks = FindObjectsOfType<WaterGimmickInAdventure>();
                foreach(var g in gimmicks)
                {
                    g.clearGimmickDelegate += ClearGimmick;
                }
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

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < passageChildren.Count; i++)
        {
            gimmickTrigger[i].gameObject.tag = "Untagged";
            gimmickTrigger[i].isTrigger = false;
        }
    }

    public void ClearGimmick()
    {
        for (int i = 0; i < gimmickTrigger.Count; i++)
        {
            gimmickTrigger[i].gameObject.tag = "Teleport";
            gimmickTrigger[i].isTrigger = true;
        }
        
        AdventureManager.isGimmickRoom = false;
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
