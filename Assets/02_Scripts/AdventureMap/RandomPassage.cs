using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wallPrefabs;
    List<Transform> passageChildren = new List<Transform>();
    int activeCount = 0;

    void Start()
    {
        foreach (Transform t in transform)
        {
            int randomIndex = Random.Range(0, wallPrefabs.Length);
            GameObject wallInMap = Instantiate(wallPrefabs[randomIndex], t);
            wallInMap.transform.SetParent(t);

            passageChildren.Add(t);
        }


        // Transform passage = transform.Find("Passage");

        // if (passage != null)
        // {
        //     foreach (Transform child in passage)
        //     {
        //         passageChildren.Add(child);
        //     }

        //     foreach (Transform child in passageChildren)
        //     {
        //         bool activate = Random.Range(0, 2) == 0;

        //         child.gameObject.SetActive(activate);
        //         if(!child.gameObject.activeSelf) activeCount++; // 비활성화 체크
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Passage object not found!");
        // }

        // if(activeCount == 4)
        // {
        //     int randomIndex = Random.Range(0, passageChildren.Count);
        //     passageChildren[randomIndex].gameObject.SetActive(true);
        // }
    }
}
